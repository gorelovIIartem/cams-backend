using System.Collections.Generic;

namespace DAL.Models
{
    public class Rule
    {
        public int RuleId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Metadata { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public ICollection<DeviceRule> DeviceRules { get; set; }
    }
}