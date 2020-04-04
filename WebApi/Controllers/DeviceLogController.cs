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
    public class DeviceLogController : ControllerBase
    {
        private readonly IDeviceLogService _deviceLogService;
        private readonly IMapper _mapper;

        public DeviceLogController(IDeviceLogService deviceLogService, IMapper mapper)
        {
            _deviceLogService = deviceLogService;
            _mapper = mapper;
        }

        [HttpPut(WebApiRoutes.DeviceLog.Create)]
        public async Task<IActionResult> CreateLog([FromBody] DeviceLogModel deviceLogModel)
        {
            DeviceLogDTO deviceLogDTO = _mapper.Map<DeviceLogDTO>(deviceLogModel);
            var actionResult = await _deviceLogService.AddLog(deviceLogDTO);
            return Ok(actionResult);
        }

        [HttpGet(WebApiRoutes.DeviceLog.SortedLogs)]
        public async Task<IActionResult> GetSortedLogsByDaterange(DateTime startDate, DateTime finishDate)
        {
            ICollection<DeviceLogModel> sortedLogs = _mapper.Map<ICollection<DeviceLogDTO>, ICollection<DeviceLogModel>>(await _deviceLogService.GetLogsSortedByDateRange(startDate, finishDate));
            return Ok(sortedLogs);
        }

        [HttpGet]
        public async Task<IActionResult> GetSortedLogsbyDeviceId(int deviceId)
        {
            ICollection<DeviceLogModel> sortedLogs = _mapper.Map<ICollection<DeviceLogDTO>, ICollection<DeviceLogModel>>(await _deviceLogService.GetSortedLogsByDeviceId(deviceId));
            return Ok(sortedLogs);
        }

        [HttpGet]
        public async Task<IActionResult> GetSortedLogsByDeviceIdAndDaterange(int deviceId, DateTime startDate, DateTime finishDate)
        {
            ICollection<DeviceLogModel> sortedLogs = _mapper.Map<ICollection<DeviceLogDTO>, ICollection<DeviceLogModel>>(await _deviceLogService.GetSortedLogsByDeviceIdAndDaterange(deviceId, startDate, finishDate));
            return Ok(sortedLogs);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAllLogsSortedByDeviceId(int deviceId)
        {
            var operationDetails = await _deviceLogService.RemoveAllLogsSortedByDeviceId(deviceId);
            return Ok(operationDetails);
        }

        [HttpGet]
        [Route(WebApiRoutes.DeviceLog.Report)]
        public async Task<HttpResponseMessage> GenerateExcelReport(int deviceId)
        {
            HttpResponseMessage responseMessage = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            responseMessage.Content = new ByteArrayContent(await _deviceLogService.GenerateReport(deviceId));
            responseMessage.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment") { FileName = "DeviceLog.xlsx" };
            responseMessage.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.speadsheetml.sheet");
            return responseMessage;
        }
    }
}
