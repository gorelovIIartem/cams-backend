using DAL.Interfaces.ModelsAbstractRepositories;
using DAL.Models;


namespace DAL.Repositories.ModelsRepositories
{
    public class DeviceRepository : BaseRepository<Device, int>, IDeviceRepository
    {
        public DeviceRepository(Microsoft.EntityFrameworkCore.DbContext context) : base(context) { }
    }
}
