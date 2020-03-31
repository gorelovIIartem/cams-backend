using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using DAL.Interfaces;
using DAL.Models;
using System;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class GroupService : IGroupService
    {
        private readonly IUnitOfWork _database;
        private readonly IMapper _mapper;
        public GroupService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _database = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ServiceActionResult> CreateUserGroup(UserGroupCreationModel userGroupCreationModel, string currentUserId)
        {
            if (userGroupCreationModel == null)
                return new ServiceActionResult { Success = false, Errors = new[] { "CreationModel is NULL" } };
            if (string.IsNullOrEmpty(userGroupCreationModel.Name))
                return new ServiceActionResult { Success = false, Errors = new[] { "Name is NULL or EMPTY!" } };

            if (!(await _database.UserRepository.CheckIfExist(currentUserId)))
                return new ServiceActionResult { Success = false, Errors = new[] { $"User with ID: {currentUserId} NOT EXISTS!" } };

            var group = new Group
            {
                Name = userGroupCreationModel.Name,
                Description = userGroupCreationModel.Description,
                UserId = currentUserId,
                CreationDate = DateTime.Now
            };

            _database.GroupRepository.Create(group);
            await _database.SaveAsync();
            return new ServiceActionResult { Success = true };
        }
    }
}
