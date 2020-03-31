using System;

namespace BLL.Models.DTOs
{
    public class DeviceLogDTO
    {
        public int LogId { get; set; }
        public int LogTypeId { get; set; }
        public int DeviceId { get; set; }
        public string Message { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
