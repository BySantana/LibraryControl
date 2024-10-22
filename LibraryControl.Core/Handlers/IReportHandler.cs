using LibraryControl.Core.Models.Reports;
using LibraryControl.Core.Requests.Reports;
using LibraryControl.Core.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryControl.Core.Handlers
{
    public interface IReportHandler
    {
        Task<Response<List<BooksByCategory>?>> GetBooksByCategoryReportAsync(GetBooksByCategoryRequest request);
        Task<Response<List<AvgBooksByMonth>?>> GetAvgBooksByMonthReportAsync(GetAvgBooksByMonthRequest request);
    }
}
