using LibraryControl.Core.Common;
using LibraryControl.Core.Handlers;
using LibraryControl.Core.Models;
using LibraryControl.Core.Requests.Books;
using LibraryControl.Core.Responses;
using System.Net.Http.Json;
using System.Transactions;

namespace LibraryControl.Web.Handlers
{
    public class BookHandler(IHttpClientFactory httpClientFactory) : IBookHandler
    {
        private readonly HttpClient _client = httpClientFactory.CreateClient(Configuration.HttpClientName);
        public async Task<Response<Book?>> CreateAsync(CreateBookRequest request)
        {
            var result = await _client.PostAsJsonAsync("v1/books", request);
            return await result.Content.ReadFromJsonAsync<Response<Book?>>()
                ?? new Response<Book?>(null, 400, "Falha ao criar livro");
        }

        public async Task<Response<Book?>> DeleteAsync(DeleteBookRequest request)
        {
            var result = await _client.DeleteAsync($"v1/books/{request.Id}");
            return await result.Content.ReadFromJsonAsync<Response<Book?>>()
                ?? new Response<Book?>(null, 400, "Falha ao excluir livro");
        }

        public async Task<PagedResponse<List<Book>?>> GetAllAsync(GetAllBooksRequest request)
        {
            return await _client.GetFromJsonAsync<PagedResponse<List<Book>?>>("v1/books")
                ?? new PagedResponse<List<Book>?>(null, 400, "Não foi possível obter os livros");
        }

        public async Task<Response<Book?>> GetByIdAsync(GetBookByIdRequest request)
        {
            return await _client.GetFromJsonAsync<Response<Book?>>($"v1/books/{request.Id}")
                ?? new Response<Book?>(null, 400, "Não foi possível obter o livro");
        }

        public async Task<PagedResponse<List<Book>?>> GetByPeriodAsync(GetBooksByPeriodRequest request)
        {
            const string format = "yyyy-MM-dd";
            var startDate = request.StartDate is not null
                ? request.StartDate.Value.ToString(format)
                : DateTime.Now.GetFirstDay().ToString(format);

            var endDate = request.EndDate is not null
                ? request.EndDate.Value.ToString(format)
                : DateTime.Now.GetLastDay().ToString(format);

            var url = $"v1/books?startDate={startDate}&endDate={endDate}";

            return await _client.GetFromJsonAsync<PagedResponse<List<Book>?>>(url)
                ?? new PagedResponse<List<Book>?>(null, 400, "Não possível obter os livros");
        }

        public async Task<Response<Book?>> UpdateAsync(UpdateBookRequest request)
        {
            var result = await _client.PutAsJsonAsync($"v1/books/{request.Id}", request);
            return await result.Content.ReadFromJsonAsync<Response<Book?>>()
                   ?? new Response<Book?>(null, 400, "Não foi possível atualizar seu livro");
        }
    }
}
