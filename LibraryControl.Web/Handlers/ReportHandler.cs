using LibraryControl.Core.Handlers;
using LibraryControl.Core.Models.Reports;
using LibraryControl.Core.Requests.Reports;
using LibraryControl.Core.Responses;
using System.Net.Http.Json;

namespace LibraryControl.Web.Handlers
{
    public class ReportHandler(IHttpClientFactory httpClientFactory) : IReportHandler
    {
        private readonly HttpClient _client = httpClientFactory.CreateClient(Configuration.HttpClientName);
        public async Task<Response<List<AvgBooksByMonth>?>> GetAvgBooksByMonthReportAsync(GetAvgBooksByMonthRequest request)
        {
            return await _client.GetFromJsonAsync<Response<List<AvgBooksByMonth>?>>("v1/reports/avgBooks")
                ?? new Response<List<AvgBooksByMonth>?>(null, 400, "Não foi possível obter os dados");
        }

        public async Task<Response<List<BooksByCategory>?>> GetBooksByCategoryReportAsync(GetBooksByCategoryRequest request)
        {
            return await _client.GetFromJsonAsync<Response<List<BooksByCategory>?>>("v1/reports/booksByCategories")
                ?? new Response<List<BooksByCategory>?>(null, 400, "Não foi possível obter os dados");
        }
    }
}
