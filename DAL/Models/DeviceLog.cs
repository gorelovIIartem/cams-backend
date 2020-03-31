using System;

namespace DAL.Models
{
    public class DeviceLog
    {
        public int LogId { get; set; }
        public int LogTypeId { get; set; }
        public int DeviceId { get; set; }
        public string Message { get; set; }
        public DateTime CreationDate { get; set; }
        public Device Device { get; set; }
        public LogType LogType { get; set; }
    }
}