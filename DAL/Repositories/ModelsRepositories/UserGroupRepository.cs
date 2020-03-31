using DAL.Interfaces.ModelsAbstractRepositories;
using DAL.Models;


namespace DAL.Repositories.ModelsRepositories
{
    public class UserGroupRepository : BaseRepository<UserGroup, int>, IUserGroupRepository
    {
        public UserGroupRepository(Microsoft.EntityFrameworkCore.DbContext context) : base(context) { }
    }
}
