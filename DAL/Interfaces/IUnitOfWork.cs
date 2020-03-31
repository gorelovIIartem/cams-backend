using DAL.Interfaces.ModelsAbstractRepositories;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IDeviceGroupRepository DeviceGroupRepository { get; }
        IDeviceLogRepository DeviceLogRepository { get; }
        IDeviceRepository DeviceRepository { get; }
        IDeviceRuleRepository DeviceRuleRepository { get; }
        IGroupRepository GroupRepository { get; }
        IRuleRepository RuleRepository { get; }
        IUserGroupRepository UserGroupRepository { get; }
        IUserRepository UserRepository { get; }

        Task SaveAsync();
    }
}