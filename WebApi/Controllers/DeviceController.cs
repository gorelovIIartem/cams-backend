using AutoMapper;
using BLL.Interfaces.DTOInterfaces;
using BLL.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using WebApi.Models.ViewModels;

namespace WebApi.Controllers
{
    [ApiController]
    public class DeviceController : ControllerBase
    {
        private readonly IDeviceService _deviceService;
        private readonly IMapper _mapper;

        public DeviceController(IDeviceService deviceService, IMapper mapper)
        {
            _deviceService = deviceService;
            _mapper = mapper;
        }

        [HttpPut(WebApiRoutes.Device.Create)]
        public async Task<IActionResult> CreateDevice([FromBody] DeviceModel deviceModel)
        {
            var actionResult = await _deviceService.AddDevice(_mapper.Map<DeviceDTO>(deviceModel));
            return Ok(actionResult);
        }
    }
}