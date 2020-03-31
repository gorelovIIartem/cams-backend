using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public class DeviceGroup
    {
        public int DeviceGroupId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public Group Group { get; set; }
        public ICollection<Device> Devices { get; set; }
    }
}