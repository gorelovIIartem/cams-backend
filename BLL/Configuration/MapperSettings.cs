using AutoMapper;
using BLL.Models.DTOs;
using DAL.Models;

namespace BLL.Configuration
{
    public class MapperSettings : Profile
    {
        public MapperSettings()
        {
            CreateMap<DeviceLogDTO, DeviceLog>().ReverseMap();
            CreateMap<GroupDTO, Group>().ReverseMap();
            CreateMap<RuleDTO, Rule>().ReverseMap();
        }
    }
}
