using LibraryControl.API.Data;
using LibraryControl.Core.Common;
using LibraryControl.Core.Handlers;
using LibraryControl.Core.Models;
using LibraryControl.Core.Requests.Books;
using LibraryControl.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace LibraryControl.API.Handlers
{
    public class BookHandler(AppDbContext context) : IBookHandler
    {
        public async Task<Response<Book?>> CreateAsync(CreateBookRequest request)
        {
            try
            {
                var book = new Book()
                {
                    UserId = request.UserId,
                    Title = request.Title,
                    Autor = request.Autor,
                    Nota = request.Nota,
                    Genre = request.Genre,
                    CategoryId = request.CategoryId,
                };

                await context.Books.AddAsync(book);
                await context.SaveChangesAsync();

                return new Response<Book?>(book, 201, "Livro criado com sucesso!");
            }
            catch
            {
                return new Response<Book?>(null, 500, "Não foi possível criar a categoria.");
            }
        }

        public async Task<Response<Book?>> DeleteAsync(DeleteBookRequest request)
        {
            try
            {
                var book = await context
                   .Books
                   .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

                if (book is null)
                    return new Response<Book?>(null, 404, "Livro não encontrada.");

                context.Books.Remove(book);
                await context.SaveChangesAsync();

                return new Response<Book?>(book, message: "Livro excluído com sucesso!");
            }
            catch
            {
                return new Response<Book?>(null, 500, "[FP080] Não foi possível excluir o livro.");
            }
        }

        public async Task<PagedResponse<List<Book>?>> GetAllAsync(GetAllBooksRequest request)
        {
            try
            {
                var query = context
                .Books
                .AsNoTracking()
                .Where(x => x.UserId == request.UserId)
                .OrderBy(x => x.Title);

                var books = await query
                    .Skip((request.PageNumber - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .ToListAsync();

                var count = await query.CountAsync();

                return new PagedResponse<List<Book>?>(
                    books,
                    count,
                    request.PageNumber,
                    request.PageSize);
            }
            catch
            {
                return new PagedResponse<List<Book>?>(null, 500, "[FP082] Não foi possível recuperar os livros.");
            }
        }

        public async Task<Response<Book?>> GetByIdAsync(GetBookByIdRequest request)
        {
            try
            {
                var book = await context
                    .Books
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

                return book is null
                    ? new Response<Book?>(null, 404, "Livro não encontrado.")
                    : new Response<Book?>(book);
            }
            catch
            {
                return new Response<Book?>(null, 500, "[FP081] Não foi possível recuperar o livro.");
            }
        }

        public async Task<PagedResponse<List<Book>?>> GetByPeriodAsync(GetBooksByPeriodRequest request)
        {
            try
            {
                request.StartDate ??= DateTime.Now.GetFirstDay();
                request.EndDate ??= DateTime.Now.GetLastDay();

            }
            catch
            {
                return new PagedResponse<List<Book>?>(null, 500, 
                    "[FP082] Não foi possível determinar a data de início ou fim.");
            }

            try
            {
                var query = context
                .Books
                .AsNoTracking()
                .Where(x => x.UserId == request.UserId &&
                            x.AddedAt >= request.StartDate &&
                            x.AddedAt <= request.EndDate)
                .OrderBy(x => x.AddedAt);

                var books = await query
                    .Skip((request.PageNumber - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .ToListAsync();

                var count = await query.CountAsync();

                return new PagedResponse<List<Book>?>(
                    books,
                    count,
                    request.PageNumber,
                    request.PageSize);
            }
            catch
            {
                return new PagedResponse<List<Book>?>(null, 500, "[FP082] Não foi possível recuperar os livros.");
            }
        }

        public async Task<Response<Book?>> UpdateAsync(UpdateBookRequest request)
        {
            try
            {
                var book = await context
                    .Books
                    .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

                if (book is null)
                    return new Response<Book?>(null, 404, "Livro não encontrada.");

                book.Title = request.Title;
                book.Autor = request.Autor;
                book.Nota = request.Nota;
                book.Genre = request.Genre;
                book.CategoryId = request.CategoryId;

                context.Books.Update(book);
                await context.SaveChangesAsync();

                return new Response<Book?>(book, message: "Livro atualizada com sucesso!");
            }
            catch
            {
                return new Response<Book?>(null, 500, "[FP079] Não foi possível atualizar o livro.");
            }
        }
    }
}
