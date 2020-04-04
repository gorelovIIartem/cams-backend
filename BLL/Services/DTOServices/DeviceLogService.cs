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

        public async Task<byte[]> GenerateReport(int deviceId)
        {
            List<DeviceLog> deviceLogs =(await _dataBase.DeviceLogRepository.GetWhereAsync(p => p.DeviceId == deviceId)).ToList();
            List<string> logTypeNames = new List<string>();
            foreach (var log in deviceLogs)
            {
                logTypeNames.Add(GetLogTypeName(log.DeviceId));
            }
            ExcelFill fill;
            Border border;
            int firstRaw = 1;
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (ExcelPackage excelPackage = new ExcelPackage())
            {
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add($"Device Information Report");
                ExcelRange excelRange = worksheet.Cells[$"A{firstRaw}:F{deviceLogs.Count() + firstRaw}"];
                excelPackage.Workbook.Properties.Created = DateTime.Now;

                excelRange.Style.Font.Bold = true;
                excelRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                excelRange.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                fill = excelRange.Style.Fill;
                fill.PatternType = ExcelFillStyle.Solid;
                fill.BackgroundColor.SetColor(Color.White);
                border = excelRange.Style.Border;
                border.Bottom.Style = border.Top.Style = border.Left.Style = border.Right.Style = ExcelBorderStyle.Thin;

                excelRange = worksheet.Cells["A1"];
                excelRange.Value = "Log Id";
                excelRange = worksheet.Cells["B1"];
                excelRange.Value = "Log type";
                excelRange = worksheet.Cells["C1"];
                excelRange.Value = "Device Id";
                excelRange = worksheet.Cells["D1"];
                excelRange.Value = "Message";
                excelRange = worksheet.Cells["E1"];
                excelRange.Value = "Creation Date";

                for (int i = firstRaw + 1; i < deviceLogs.Count() + 1; i++)
                {

                    excelRange = worksheet.Cells[$"A{i}"];
                    excelRange.Value = deviceLogs[i - (firstRaw + 1)].LogId;
                    excelRange = worksheet.Cells[$"B{i}"];
                    excelRange.Value = logTypeNames[i - (firstRaw + 1)];
                    excelRange = worksheet.Cells[$"C{i}"];
                    excelRange.Value = deviceLogs[i - (firstRaw + 1)].DeviceId;
                    excelRange = worksheet.Cells[$"D{i}"];
                    excelRange.Value = deviceLogs[i - (firstRaw + 1)].Message;
                    excelRange = worksheet.Cells[$"E{i}"];
                    excelRange.Value = deviceLogs[i - (firstRaw + 1)].CreationDate;
                }
                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();
                return await excelPackage.GetAsByteArrayAsync();
            }

        }

        private string GetLogTypeName(int logTypeId)
        {
            return Enum.GetName(typeof(DAL.Helpers.Enums.LogType), logTypeId);
        }
    }
}
