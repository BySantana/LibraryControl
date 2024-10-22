using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryControl.Core.Models.Reports
{
    public record BooksByCategory(string UserId, string Category, int Year, int Books);
}
