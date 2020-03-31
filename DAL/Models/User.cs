using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace DAL.Models
{
    public class User : IdentityUser
    {
        public ICollection<UserGroup> UserGroups { get; set; }
        public ICollection<Device> Devices { get; set; }
        public ICollection<Group> Groups { get; set; }
        public ICollection<Rule> Rules { get; set; }
    }
}
