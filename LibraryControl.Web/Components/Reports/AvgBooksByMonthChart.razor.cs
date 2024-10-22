using LibraryControl.Core.Handlers;
using LibraryControl.Core.Requests.Reports;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Globalization;

namespace LibraryControl.Web.Components.Reports
{
    public class AvgBooksByMonthChartComponent : ComponentBase
    {
        #region Properties

        public ChartOptions Options { get; set; } = new();
        public List<ChartSeries>? Series { get; set; }
        public List<string> Labels { get; set; } = [];

        #endregion

        #region Services

        [Inject]
        public IReportHandler Handler { get; set; } = null!;

        [Inject]
        public ISnackbar Snackbar { get; set; } = null!;

        #endregion

        #region Override

        protected override async Task OnInitializedAsync()
        {
            var request = new GetAvgBooksByMonthRequest();
            var result = await Handler.GetAvgBooksByMonthReportAsync(request);
            if (!result.IsSuccess || result.Data is null)
            {
                Snackbar.Add("Não foi possível obter os dados do relatório", Severity.Error);
                return;
            }

            var averages = new List<double>();

            foreach (var item in result.Data)
            {
                averages.Add(item.AverageNotas);
                Labels.Add(GetMonthName(item.Month));
                Console.WriteLine(item.AverageNotas.ToString());
            }

            Options.YAxisTicks = 10;
            Options.LineStrokeWidth = 5;
            Options.ChartPalette = ["#76FF01", Colors.Red.Default];
            Series =
            [
                new ChartSeries { Name = "Nota média", Data = averages.ToArray() },
            ];

            StateHasChanged();
        }

        #endregion

        private static string GetMonthName(int month)
            => new DateTime(DateTime.Now.Year, month, 1)
                .ToString("MMMM", CultureInfo.CurrentCulture);
    }
}
