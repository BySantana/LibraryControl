using LibraryControl.API.Data;
using LibraryControl.Core.Handlers;
using LibraryControl.Core.Models.Reports;
using LibraryControl.Core.Requests.Reports;
using LibraryControl.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace LibraryControl.API.Handlers
{
    public class ReportHandler(AppDbContext context) : IReportHandler
    {
        public async Task<Response<List<AvgBooksByMonth>?>> GetAvgBooksByMonthReportAsync(GetAvgBooksByMonthRequest request)
        {
            try
            {
                var data = await context
                .AvgBooksByMonths
                .AsNoTracking()
                .Where(x => x.UserId == request.UserId)
                .OrderByDescending(x => x.Year)
                .ThenBy(x => x.Month)
                .ToListAsync();

                return new Response<List<AvgBooksByMonth>?>(data);
            }
            catch
            {
                return new Response<List<AvgBooksByMonth>?>(null, 500, "[FP033] Não foi possível obter as médias das notas dos livros");
            }
            
        }

        public async Task<Response<List<BooksByCategory>?>> GetBooksByCategoryReportAsync(GetBooksByCategoryRequest request)
        {
            try
            {
                var data = await context
                 .BooksByCategories
                 .AsNoTracking()
                 .Where(x => x.UserId == request.UserId)
                 .OrderByDescending(x => x.Year)
                 .ToListAsync();

                return new Response<List<BooksByCategory>?>(data);
            }
            catch
            {
                return new Response<List<BooksByCategory>?>(null, 500, " Não foi possível obter os livros pelas categorias.");
            }
            
        }
    }
}
