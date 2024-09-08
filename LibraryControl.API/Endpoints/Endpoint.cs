using LibraryControl.API.Common.Api;
using LibraryControl.API.Endpoints.Books;
using LibraryControl.API.Endpoints.Categories;
using LibraryControl.API.Endpoints.Identity;
using LibraryControl.API.Models;

namespace LibraryControl.API.Endpoints
{
    public static class Endpoint
    {
        public static void MapEndpoints(this WebApplication app)
        {
            var endpoints = app.MapGroup("");

            endpoints.MapGroup("/")
                .WithTags("Health Check")
                .MapGet("/", () => new { message = "OK" });

            endpoints.MapGroup("v1/categories")
                .WithTags("Categories")
                .RequireAuthorization()
                .MapEndpoint<CreateCategoryEndpoint>()
                .MapEndpoint<UpdateCategoryEndpoint>()
                .MapEndpoint<DeleteCategoryEndpoint>()
                .MapEndpoint<GetCategoryByIdEndpoint>()
                .MapEndpoint<GetAllCategoriesEndpoint>();

            endpoints.MapGroup("v1/books")
                .WithTags("Books")
                .RequireAuthorization()
                .MapEndpoint<CreateBookEndpoint>()
                .MapEndpoint<UpdateBookEndpoint>()
                .MapEndpoint<DeleteBookEndpoint>()
                .MapEndpoint<GetBookByIdEndpoint>()
                .MapEndpoint<GetAllBooksEndpoint>()
                .MapEndpoint<GetBooksByPeriodEndpoint>();

            endpoints.MapGroup("v1/identity")
                .WithTags("Identity")
                .MapIdentityApi<User>();

            endpoints.MapGroup("v1/identity")
                .WithTags("Identity")
                .RequireAuthorization()
                .MapEndpoint<LogoutEndpoint>()
                .MapEndpoint<GetRolesEndpoint>();
        }

        private static IEndpointRouteBuilder MapEndpoint<TEndpoint>(this IEndpointRouteBuilder app)
            where TEndpoint : IEndpoint
        {
            TEndpoint.Map(app);
            return app;
        }
    }
}
