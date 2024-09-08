using LibraryControl.Core.Common;
using LibraryControl.Core.Handlers;
using LibraryControl.Core.Models;
using LibraryControl.Core.Requests.Books;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Transactions;

namespace LibraryControl.Web.Pages.Books
{
    public partial class ListBooksPage : ComponentBase
    {
        #region Properties

        public bool IsBusy { get; set; } = false;
        public List<Book> Books { get; set; } = [];
        public string SearchTerm { get; set; } = string.Empty;
        public int CurrentYear { get; set; } = DateTime.Now.Year;
        public int CurrentMonth { get; set; } = DateTime.Now.Month;

        public int[] Years { get; set; } =
        {
            DateTime.Now.Year,
            DateTime.Now.AddYears(-1).Year,
            DateTime.Now.AddYears(-2).Year,
            DateTime.Now.AddYears(-3).Year
        };

        #endregion

        #region Services

        [Inject]
        public ISnackbar Snackbar { get; set; } = null!;

        [Inject]
        public IDialogService DialogService { get; set; } = null!;

        [Inject]
        public IBookHandler Handler { get; set; } = null!;

        #endregion

        #region Overrides

        protected override async Task OnInitializedAsync()
            => await GetBooksAsync();

        #endregion

        #region Public Methods

        public async Task OnSearchAsync()
        {
            await GetBooksAsync();
            StateHasChanged();
        }

        public async void OnDeleteButtonClickedAsync(long id, string title)
        {
            var result = await DialogService.ShowMessageBox(
                "ATENÇÃO",
                $"Ao prosseguir o livro {title} será excluído. Esta ação é irreversível! Deseja continuar?",
                yesText: "EXCLUIR",
                cancelText: "Cancelar");

            if (result is true)
                await OnDeleteAsync(id, title);

            StateHasChanged();
        }

        public Func<Book, bool> Filter => book =>
        {
            if (string.IsNullOrEmpty(SearchTerm))
                return true;

            return book.Id.ToString().Contains(SearchTerm, StringComparison.OrdinalIgnoreCase)
                   || book.Title.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase);
        };

        #endregion

        #region Private Methods

        private async Task GetBooksAsync()
        {
            IsBusy = true;

            try
            {
                var request = new GetBooksByPeriodRequest
                {
                    StartDate = DateTime.Now.GetFirstDay(CurrentYear, CurrentMonth),
                    EndDate = DateTime.Now.GetLastDay(CurrentYear, CurrentMonth),
                    PageNumber = 1,
                    PageSize = 1000
                };
                var result = await Handler.GetByPeriodAsync(request);
                if (result.IsSuccess)
                    Books = result.Data ?? [];
            }
            catch (Exception ex)
            {
                Snackbar.Add(ex.Message, Severity.Error);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task OnDeleteAsync(long id, string title)
        {
            IsBusy = true;

            try
            {
                var result = await Handler.DeleteAsync(new DeleteBookRequest { Id = id });
                if (result.IsSuccess)
                {
                    Snackbar.Add($"Livro {title} removido!", Severity.Success);
                    Books.RemoveAll(x => x.Id == id);
                }
                else
                {
                    Snackbar.Add(result.Message, Severity.Error);
                }
            }
            catch (Exception ex)
            {
                Snackbar.Add(ex.Message, Severity.Error);
            }
            finally
            {
                IsBusy = false;
            }
        }

        #endregion
    }
}
