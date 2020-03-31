using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public class Group
    {
        public int GroupId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public ICollection<UserGroup> UserGroups { get; set; }
    }
}