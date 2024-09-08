using LibraryControl.API.Common.Api;
using LibraryControl.API.Models;
using LibraryControl.Core.Handlers;
using LibraryControl.Core.Models;
using LibraryControl.Core.Requests.Books;
using LibraryControl.Core.Responses;
using System.Security.Claims;

namespace LibraryControl.API.Endpoints.Books
{
    public class UpdateBookEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapPut("/{id}", HandleAsync)
            .WithName("Books: Update")
            .WithSummary("Atualiza uma livro")
            .WithDescription("Atualiza uma livro")
            .WithOrder(1)
            .Produces<Response<Book?>>();


        private static async Task<IResult> HandleAsync(
            ClaimsPrincipal user,
            long id,
            IBookHandler handler,
            UpdateBookRequest request)
        {
            request.UserId = user.Identity?.Name ?? string.Empty;
            request.Id = id;

            var result = await handler.UpdateAsync(request);

            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);


        }
    }
}
