using System.Collections.Generic;

namespace DAL.Models
{
    public class LogType
    {
        public int LogTypeId { get; set; }
        public string LogName { get; set; }
        public string Description { get; set; }
        public ICollection<DeviceLog> Logs { get; set; }
    }
}