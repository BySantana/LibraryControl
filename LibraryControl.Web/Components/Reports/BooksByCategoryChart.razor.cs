using LibraryControl.Core.Handlers;
using LibraryControl.Core.Requests.Reports;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace LibraryControl.Web.Components.Reports
{
    public class BooksByCategoryChartComponent : ComponentBase
    {
        #region Properties

        public List<double> Data { get; set; } = [];
        public List<string> Labels { get; set; } = [];

        #endregion

        #region Services

        [Inject]
        public IReportHandler Handler { get; set; } = null!;

        [Inject]
        public ISnackbar Snackbar { get; set; } = null!;

        #endregion

        #region Overrides

        protected override async Task OnInitializedAsync()
        {
            await GetBooksByCategoryAsync();
        }

        private async Task GetBooksByCategoryAsync()
        {
            var request = new GetBooksByCategoryRequest();
            var result = await Handler.GetBooksByCategoryReportAsync(request);
            if (!result.IsSuccess || result.Data is null)
            {
                Snackbar.Add("Falha ao obter dados do relatório", Severity.Error);
                return;
            }

            foreach (var item in result.Data)
            {
                Labels.Add($"{item.Category} ({item.Books})");
                Data.Add(item.Books);
            }
        }

        #endregion
    }
}
