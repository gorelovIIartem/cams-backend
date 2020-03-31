using System;

namespace BLL.Models.DTOs
{
    public class GroupDTO
    {
        public int GroupId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public string UserId { get; set; }
    }
}
