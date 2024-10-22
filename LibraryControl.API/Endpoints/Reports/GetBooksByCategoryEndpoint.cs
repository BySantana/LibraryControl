using LibraryControl.API.Common.Api;
using LibraryControl.Core.Handlers;
using LibraryControl.Core.Models.Reports;
using LibraryControl.Core.Requests.Categories;
using LibraryControl.Core.Requests.Reports;
using LibraryControl.Core.Responses;
using System.Security.Claims;

namespace LibraryControl.API.Endpoints.Reports
{
    public class GetBooksByCategoryEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapGet("/booksByCategories", HandleAsync)
                .Produces<Response<List<BooksByCategory>?>>();

        private static async Task<IResult> HandleAsync(
            ClaimsPrincipal user,
            IReportHandler handler)
        {
            var request = new GetBooksByCategoryRequest
            {
                UserId = user.Identity?.Name ?? string.Empty,
            };

            var result = await handler.GetBooksByCategoryReportAsync(request);
            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }
    }
}
