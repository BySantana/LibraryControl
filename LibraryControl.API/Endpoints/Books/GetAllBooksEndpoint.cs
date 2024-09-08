using LibraryControl.API.Common.Api;
using LibraryControl.Core.Handlers;
using LibraryControl.Core.Models;
using LibraryControl.Core;
using Microsoft.AspNetCore.Mvc;
using LibraryControl.Core.Responses;
using LibraryControl.Core.Requests.Books;
using LibraryControl.API.Models;
using System.Security.Claims;

namespace LibraryControl.API.Endpoints.Books
{
    public class GetAllBooksEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/{pageNumber}/{pageSize}", HandleAsync)
            .WithName("Books: Get All")
            .WithSummary("Recupera todos os livros")
            .WithDescription("Recupera todos os livros")
            .WithOrder(1)
            .Produces<Response<Book?>>();

        private static async Task<IResult> HandleAsync(
            ClaimsPrincipal user,
            IBookHandler handler,
            [FromQuery] int pageNumber = Configuration.DefaultPageNumber,
            [FromQuery] int pageSize = Configuration.DefaultPageSize)
        {

            var request = new GetAllBooksRequest
            {
                UserId = user.Identity?.Name ?? string.Empty,
                PageSize = pageSize,
                PageNumber = pageNumber
            };

            var result = await handler.GetAllAsync(request);

            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }
    }
}
