using DAL.Interfaces.ModelsAbstractRepositories;
using DAL.Models;


namespace DAL.Repositories.ModelsRepositories
{
    public class DeviceLogRepository : BaseRepository<DeviceLog, int>, IDeviceLogRepository
    {
        public DeviceLogRepository(Microsoft.EntityFrameworkCore.DbContext context) : base(context) { }
    }
}
