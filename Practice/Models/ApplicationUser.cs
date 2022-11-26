using Microsoft.AspNetCore.Identity;
using System.Runtime.Serialization;

namespace WorkHours.Models
{
    public class ApplicationUser :IdentityUser
    {
        public string DisplayName { get; set; }
    }
}
