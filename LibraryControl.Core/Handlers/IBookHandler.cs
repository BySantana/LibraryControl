using LibraryControl.Core.Models;
using LibraryControl.Core.Requests.Books;
using LibraryControl.Core.Responses;

namespace LibraryControl.Core.Handlers
{
    public interface IBookHandler
    {
        Task<Response<Book?>> CreateAsync(CreateBookRequest request);
        Task<Response<Book?>> UpdateAsync(UpdateBookRequest request);
        Task<Response<Book?>> DeleteAsync(DeleteBookRequest request);
        Task<Response<Book?>> GetByIdAsync(GetBookByIdRequest request);
        Task<PagedResponse<List<Book>?>> GetAllAsync(GetAllBooksRequest request);
        Task<PagedResponse<List<Book>?>> GetByPeriodAsync(GetBooksByPeriodRequest request);
    }
}
