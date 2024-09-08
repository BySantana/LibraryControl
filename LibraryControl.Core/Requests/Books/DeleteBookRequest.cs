using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryControl.Core.Requests.Books
{
    public class DeleteBookRequest : Request
    {
        public long Id { get; set; }
    }
}
