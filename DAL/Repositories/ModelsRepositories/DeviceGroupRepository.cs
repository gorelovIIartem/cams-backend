using DAL.Interfaces.ModelsAbstractRepositories;
using DAL.Models;

namespace DAL.Repositories.ModelsRepositories
{
    public class DeviceGroupRepository : BaseRepository<DeviceGroup, int>, IDeviceGroupRepository
    {
        public DeviceGroupRepository(Microsoft.EntityFrameworkCore.DbContext context) : base(context) { }
    }
}
