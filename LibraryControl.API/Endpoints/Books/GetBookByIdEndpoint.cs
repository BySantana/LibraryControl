using LibraryControl.API.Common.Api;
using LibraryControl.API.Models;
using LibraryControl.Core.Handlers;
using LibraryControl.Core.Models;
using LibraryControl.Core.Requests.Books;
using LibraryControl.Core.Responses;
using System.Security.Claims;

namespace LibraryControl.API.Endpoints.Books
{
    public class GetBookByIdEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapGet("/{id}", HandleAsync)
            .WithName("Books: Get")
            .WithSummary("Recupera uma livro")
            .WithDescription("Recupera uma livro")
            .WithOrder(1)
            .Produces<Response<Book?>>();

        private static async Task<IResult> HandleAsync(
            ClaimsPrincipal user,
            long id,
            IBookHandler handler)
        {
            var request = new GetBookByIdRequest
            {
                UserId = user.Identity?.Name ?? string.Empty,
                Id = id
            };

            var result = await handler.GetByIdAsync(request);

            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }
    }
}
