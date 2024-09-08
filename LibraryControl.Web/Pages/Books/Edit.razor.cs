using LibraryControl.Core.Handlers;
using LibraryControl.Core.Models;
using LibraryControl.Core.Requests.Books;
using LibraryControl.Core.Requests.Categories;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace LibraryControl.Web.Pages.Books
{
    public class EditBookPage : ComponentBase
    {
        #region Properties

        [Parameter]
        public string Id { get; set; } = string.Empty;

        public bool IsBusy { get; set; } = false;
        public UpdateBookRequest InputModel { get; set; } = new();
        public List<Category> Categories { get; set; } = [];

        #endregion

        #region Services

        [Inject]
        public IBookHandler BookHandler { get; set; } = null!;

        [Inject]
        public ICategoryHandler CategoryHandler { get; set; } = null!;

        [Inject]
        public NavigationManager NavigationManager { get; set; } = null!;

        [Inject]
        public ISnackbar Snackbar { get; set; } = null!;

        #endregion

        #region Overrides

        protected override async Task OnInitializedAsync()
        {
            IsBusy = true;

            await GetBookByIdAsync();
            await GetCategoriesAsync();

            IsBusy = false;
        }

        #endregion

        #region Methods

        public async Task OnValidSubmitAsync()
        {
            IsBusy = true;

            try
            {
                var result = await BookHandler.UpdateAsync(InputModel);
                if (result.IsSuccess)
                {
                    Snackbar.Add("Livro atualizado", Severity.Success);
                    NavigationManager.NavigateTo("/lancamentos/historico");
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

        #region Private Methods

        private async Task GetBookByIdAsync()
        {
            IsBusy = true;
            try
            {
                var request = new GetBookByIdRequest { Id = long.Parse(Id) };
                var result = await BookHandler.GetByIdAsync(request);
                if (result is { IsSuccess: true, Data: not null })
                {
                    InputModel = new UpdateBookRequest
                    {
                        CategoryId = result.Data.CategoryId,
                        Title = result.Data.Title,
                        Genre = result.Data.Genre,
                        Autor = result.Data.Autor,
                        Nota = result.Data.Nota,
                        Id = result.Data.Id
                    };
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

        private async Task GetCategoriesAsync()
        {
            IsBusy = true;
            try
            {
                var request = new GetAllCategoriesRequest();
                var result = await CategoryHandler.GetAllAsync(request);
                if (result.IsSuccess)
                {
                    Categories = result.Data ?? [];
                    InputModel.CategoryId = Categories.FirstOrDefault()?.Id ?? 0;
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
