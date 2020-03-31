namespace DAL.Models
{
    public class UserGroup
    {
        public string UserId { get; set; }
        public int GroupId { get; set; }

        public User User { get; set; }
        public Group Group { get; set; }
    }
}
