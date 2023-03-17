using Microsoft.AspNetCore.Identity;

namespace LMS_API_DataLayer.Models.User
{

    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
    }
}
