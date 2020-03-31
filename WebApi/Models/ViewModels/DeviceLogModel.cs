using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models.ViewModels
{
    public class DeviceLogModel
    {
        public int LogId { get; set; }
        public int LogTypeId { get; set; }
        public int DeviceId { get; set; }
        public string Message { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
