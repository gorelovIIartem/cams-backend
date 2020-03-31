namespace DAL.Models
{
    public class DeviceRule
    {
        public int RuleId { get; set; }
        public int DeviceId { get; set; }
        public Rule Rule { get; set; }
        public Device Device { get; set; }
    }
}