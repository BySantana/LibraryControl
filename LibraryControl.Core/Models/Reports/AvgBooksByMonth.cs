using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryControl.Core.Models.Reports
{
    public record AvgBooksByMonth(string UserId, double AverageNotas, int Month, int Year);
}
