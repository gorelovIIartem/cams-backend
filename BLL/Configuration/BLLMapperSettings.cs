using AutoMapper;
using BLL.Models.DTOs;
using DAL.Models;

namespace BLL.Configuration
{
    public class BLLMapperSettings : Profile
    {
        public BLLMapperSettings()
        {
            CreateMap<DeviceLogDTO, DeviceLog>().ReverseMap();
            CreateMap<RuleDTO, Rule>().ReverseMap();
            CreateMap<DeviceDTO, Device>().ReverseMap();
        }
    }
}
