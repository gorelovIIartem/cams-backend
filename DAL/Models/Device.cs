using System;
using System.Collections.Generic;
using DAL.Helpers.Enums;

namespace DAL.Models
{
    public class Device
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
        public User User { get; set; }
        public DeviceType DeviceType { get; set; }
        public ICollection<DeviceLog> DeviceLogs { get; set; }
        public ICollection<DeviceRule> DeviceRules { get; set; }
    }
}