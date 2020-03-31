using DAL.Interfaces.ModelsAbstractRepositories;
using DAL.Models;


namespace DAL.Repositories.ModelsRepositories
{
    public class DeviceRuleRepository : BaseRepository<DeviceRule, int>, IDeviceRuleRepository
    {
        public DeviceRuleRepository(Microsoft.EntityFrameworkCore.DbContext context) : base(context) { }
    }
}
