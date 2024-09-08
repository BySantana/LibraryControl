using LibraryControl.API.Common.Api;
using LibraryControl.Core.Handlers;
using LibraryControl.Core.Models;
using LibraryControl.Core.Requests.Books;
using LibraryControl.Core.Requests.Categories;
using LibraryControl.Core.Responses;
using System.Security.Claims;

namespace LibraryControl.API.Endpoints.Books
{
    public class CreateBookEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
         => app.MapPost("/", HandleAsync)
            .WithName("Books: Create")
            .WithSummary("Cria um novo livro")
            .WithDescription("Cria um novo livro")
            .WithOrder(1)
            .Produces<Response<Book?>>();

        private static async Task<IResult> HandleAsync(
            ClaimsPrincipal user,
            IBookHandler handler,
            CreateBookRequest request)
        {
            request.UserId = user.Identity?.Name ?? string.Empty;
            var result = await handler.CreateAsync(request);

            return result.IsSuccess
                ? TypedResults.Created($"/{result.Data?.Id}", result)
                : TypedResults.BadRequest(result);

        }
    }
}
