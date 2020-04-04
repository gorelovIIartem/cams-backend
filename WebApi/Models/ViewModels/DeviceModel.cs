using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models.ViewModels
{
    public class DeviceModel
    {
        public int DeviceId { get; set; }
        public string DeviceName { get; set; }
        public string MAC { get; set; }
        public string EndPoint { get; set; }
        public string ControlParameter { get; set; }
        public string Description { get; set; }
        public double MinValue { get; set; }
        public double MaxValue { get; set; }
        public string DeviceSettings { get; set; }
        public DateTime CreationDate { get; set; }
        public string UserId { get; set; }
    }
}
