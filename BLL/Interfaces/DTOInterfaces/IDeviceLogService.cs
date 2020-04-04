using BLL.Infrastructure;
using BLL.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Interfaces.DTOInterfaces
{
    public interface IDeviceLogService
    {
        Task<OperationDetails> AddLog(DeviceLogDTO logDTO);
        Task<ICollection<DeviceLogDTO>> GetLogsSortedByDateRange(DateTime startDate, DateTime finishDate);
        Task<ICollection<DeviceLogDTO>> GetSortedLogsByDeviceId(int deviceId);
        Task<ICollection<DeviceLogDTO>> GetSortedLogsByDeviceIdAndDaterange(int deviceId, DateTime startDate, DateTime finishDate);
        Task<OperationDetails> RemoveAllLogsSortedByDeviceId(int deviceId);
        Task<byte[]> GenerateReport(int deviceId);
    }
}
