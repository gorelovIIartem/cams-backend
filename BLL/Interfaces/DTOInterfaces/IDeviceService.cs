using BLL.Infrastructure;
using BLL.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Interfaces.DTOInterfaces
{
    public interface IDeviceService
    {
        Task<OperationDetails> AddDevice(DeviceDTO deviceDTO);
    }
}
