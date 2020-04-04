using AutoMapper;
using BLL.Models.DTOs;
using WebApi.Models.ViewModels;

namespace WebApi.Configuration
{
    public class WebApiMapperSettings : Profile
    {
        public WebApiMapperSettings()
        {
            CreateMap<DeviceLogModel, DeviceLogDTO>().ReverseMap();
            CreateMap<RuleModel, RuleDTO>().ReverseMap();
            CreateMap<DeviceModel, DeviceDTO>().ReverseMap();
        }
    }
}
