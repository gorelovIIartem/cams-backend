using AutoMapper;
using BLL.Infrastructure;
using BLL.Interfaces.DTOInterfaces;
using BLL.Models.DTOs;
using DAL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Services.DTOServices
{
    public class DeviceLogService : IDeviceLogService
    {
        private readonly IUnitOfWork _dataBase;
        private readonly IMapper _mapper;

        public DeviceLogService(IUnitOfWork uow, IMapper mapper)
        {
            _dataBase = uow;
            _mapper = mapper;
        }
        public async Task<OperationDetails> AddLog(DeviceLogDTO logDTO)
        {
            if (logDTO == null)
                throw new ValidationException("There is no information about target log.", "Empty input parameter.");
            if (await _dataBase.DeviceLogRepository.CheckIfExist(logDTO.LogId))
                throw new ValidationException("Log with this id already exists.", logDTO.LogId.ToString());
            _dataBase.DeviceLogRepository.Create(_mapper.Map<DeviceLog>(logDTO));
            await _dataBase.SaveAsync();
            return new OperationDetails(true, "Log created succesfully", logDTO.LogId.ToString());
        }

        public async Task<ICollection<DeviceLogDTO>> GetLogsSortedByDateRange(DateTime startDate, DateTime finishDate)
        {
            if (startDate > finishDate)
                throw new ValidationException("Start date is bigger then finish date.", "Invalid input parameter");
            ICollection<DeviceLogDTO> sortedLogs = _mapper.Map<ICollection<DeviceLogDTO>>(await _dataBase.DeviceLogRepository.GetAllIncludingAsync(p => p.CreationDate >= startDate & p.CreationDate <= finishDate));
            return sortedLogs;
        }

        public async Task<ICollection<DeviceLogDTO>> GetSortedLogsByDeviceId(int deviceId)
        {
            if (await _dataBase.DeviceLogRepository.CheckIfExist(deviceId) == false)
                throw new ValidationException("There is no information about target device.", $"Incorrect deviceId - {deviceId.ToString()}");
            ICollection<DeviceLogDTO> sortedLogs;
            Device device = (await _dataBase.DeviceRepository.GetWhereAsync(p => p.DeviceId == deviceId & p.DeviceType == DAL.Helpers.Enums.DeviceType.Main)).FirstOrDefault();
            if (device.DeviceType == DAL.Helpers.Enums.DeviceType.Main)
            {
                sortedLogs = _mapper.Map<ICollection<DeviceLogDTO>>(await _dataBase.DeviceLogRepository.GetAllIncludingAsync(p => p.DeviceId == deviceId));
            }
            else
                throw new ValidationException("Target device has not rights for this operation.Please, select main device.", $"{deviceId.ToString()}");
            return sortedLogs;
        }

        public async Task<ICollection<DeviceLogDTO>> GetSortedLogsByDeviceIdAndDaterange(int deviceId, DateTime startDate, DateTime finishDate)
        {
            if (await _dataBase.DeviceLogRepository.CheckIfExist(deviceId) == false)
                throw new ValidationException("There is no information about target device.", $"Incorrect deviceId - {deviceId.ToString()}");
            ICollection<DeviceLogDTO> sortedLogs;
            Device device = (await _dataBase.DeviceRepository.GetWhereAsync(p => p.DeviceId == deviceId & p.DeviceType == DAL.Helpers.Enums.DeviceType.Main)).FirstOrDefault();
            if (device.DeviceType == DAL.Helpers.Enums.DeviceType.Main)
            {
                sortedLogs = _mapper.Map<ICollection<DeviceLogDTO>>(await _dataBase.DeviceLogRepository.GetAllIncludingAsync(p => p.DeviceId == deviceId & p.CreationDate >= startDate & p.CreationDate <= finishDate));
            }
            else
                throw new ValidationException("Target device has not rights for this operation.Please, select main device.", $"{deviceId.ToString()}");
            return sortedLogs;
        }

        public async Task<OperationDetails> RemoveAllLogsSortedByDeviceId(int deviceId)
        {
            if (await _dataBase.DeviceRepository.CheckIfExist(deviceId) == false)
                throw new ValidationException("There is no information about target device.", $"Incorrect deviceId - {deviceId.ToString()} ");
            ICollection<DeviceLog> sortedLogs = await _dataBase.DeviceLogRepository.GetAllIncludingAsync(p => p.DeviceId == deviceId);
            _dataBase.DeviceLogRepository.DeleteSeveral(sortedLogs);
            return new OperationDetails(true, $"Logs for {deviceId.ToString()} removed.", $"Target device - {deviceId.ToString()}");
        }
    }
}
