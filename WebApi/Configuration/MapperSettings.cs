using AutoMapper;
using BLL.Models.DTOs;
using WebApi.Models.ViewModels;

namespace BLL.Configuration
{
    public class MapperSettings : Profile
    {
        public MapperSettings()
        {
            CreateMap<DeviceLogModel, DeviceLogDTO>().ReverseMap();
            CreateMap<RuleModel, RuleDTO>().ReverseMap();
        }
    }
}
