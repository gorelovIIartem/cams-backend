using DAL.Interfaces;
using DAL.Interfaces.ModelsAbstractRepositories;
using DAL.Repositories.ModelsRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly DbContext _context;

        private IDeviceGroupRepository _deviceGroupRepository;
        private IDeviceLogRepository _deviceLogRepository;
        private IDeviceRepository _deviceRepository;
        private IDeviceRuleRepository _deviceRulerepository;
        private IGroupRepository _groupRepository;
        private IRuleRepository _ruleRepository;
        private IUserGroupRepository _userGroupRepository;
        private IUserRepository _userRepository;

        public UnitOfWork(DbContext dbContext)
        {
            _context = dbContext;
        }

        public IDeviceGroupRepository DeviceGroupRepository
        {
            get
            {
                if (_deviceGroupRepository == null)
                    _deviceGroupRepository = new DeviceGroupRepository(_context);
                return _deviceGroupRepository;
            }
        }

        public IDeviceLogRepository DeviceLogRepository
        {
            get
            {
                if (_deviceLogRepository == null)
                    _deviceLogRepository = new DeviceLogRepository(_context);
                return _deviceLogRepository;
            }
        }

        public IDeviceRepository DeviceRepository
        {
            get
            {
                if (_deviceRepository == null)
                    _deviceRepository = new DeviceRepository(_context);
                return _deviceRepository;
            }
        }

        public IDeviceRuleRepository DeviceRuleRepository
        {
            get
            {
                if (_deviceRulerepository == null)
                    _deviceRulerepository = new DeviceRuleRepository(_context);
                return _deviceRulerepository;
            }
        }

        public IGroupRepository GroupRepository
        {
            get
            {
                if (_groupRepository == null)
                    _groupRepository = new GroupRepository(_context);
                return _groupRepository;
            }
        }

        public IRuleRepository RuleRepository
        {
            get
            {
                if (_ruleRepository == null)
                    _ruleRepository = new RuleRepository(_context);
                return _ruleRepository;
            }
        }

        public IUserGroupRepository UserGroupRepository
        {
            get
            {
                if (_userGroupRepository == null)
                    _userGroupRepository = new UserGroupRepository(_context);
                return _userGroupRepository;
            }
        }

        public IUserRepository UserRepository
        {
            get
            {
                if (_userRepository == null)
                    _userRepository = new UserRepository(_context);
                return _userRepository;
            }
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                disposedValue = true;
            }
        }

        ~UnitOfWork()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}