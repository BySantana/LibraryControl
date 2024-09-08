using LibraryControl.API.Common.Api;
using LibraryControl.Core.Handlers;
using LibraryControl.Core.Models;
using LibraryControl.Core.Requests.Books;
using LibraryControl.Core;
using Microsoft.AspNetCore.Mvc;
using LibraryControl.Core.Responses;
using LibraryControl.API.Models;
using System.Security.Claims;

namespace LibraryControl.API.Endpoints.Books
{
    public class GetBooksByPeriodEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/", HandleAsync)
            .WithName("Books: Get By Period")
            .WithSummary("Recupera todos os livros de acordo com a data")
            .WithDescription("Recupera todos os livros de acordo com a data")
            .WithOrder(1)
            .Produces<Response<Book?>>();

        private static async Task<IResult> HandleAsync(
            ClaimsPrincipal user,
            IBookHandler handler,
            [FromQuery] DateTime? startDate,
            [FromQuery] DateTime? endDate,
            [FromQuery] int pageNumber = Configuration.DefaultPageNumber,
            [FromQuery] int pageSize = Configuration.DefaultPageSize
            )
        {

            var request = new GetBooksByPeriodRequest
            {
                UserId = user.Identity?.Name ?? string.Empty,
                PageSize = pageSize,
                PageNumber = pageNumber,
                StartDate = startDate,
                EndDate = endDate
            };

            var result = await handler.GetByPeriodAsync(request);

            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }
    }
}
