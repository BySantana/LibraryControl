using Microsoft.AspNetCore.Identity;

namespace LibraryControl.API.Models
{
    public class User : IdentityUser<long>
    {
        public List<IdentityRole<long>>? Roles { get; set; }
    }
}
