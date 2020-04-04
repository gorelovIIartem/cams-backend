using AutoMapper;
using BLL.Infrastructure;
using BLL.Interfaces.DTOInterfaces;
using BLL.Models.DTOs;
using DAL.Interfaces;
using DAL.Models;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Services.DTOServices
{
    public class DeviceService : IDeviceService
    {

        private readonly IUnitOfWork _dataBase;
        private readonly IMapper _mapper;

        public DeviceService(IUnitOfWork uow, IMapper mapper)
        {
            _dataBase = uow;
            _mapper = mapper;
        }

        public async Task<OperationDetails> AddDevice(DeviceDTO deviceDTO)
        {
            if (deviceDTO == null)
                throw new ValidationException("There is no information about target device.", "Empty input parameter.");
            if (await _dataBase.DeviceRepository.CheckIfExist(deviceDTO.DeviceId))
                throw new ValidationException("Device with this id already exists.", deviceDTO.DeviceId.ToString());
            _dataBase.DeviceRepository.Create(_mapper.Map<DeviceDTO, Device>(deviceDTO));
            await _dataBase.SaveAsync();
            return new OperationDetails(true, "Device created succesfully", deviceDTO.DeviceId.ToString());
        }
    }
}
