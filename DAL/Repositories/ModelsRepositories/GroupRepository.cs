using DAL.Interfaces.ModelsAbstractRepositories;
using DAL.Models;


namespace DAL.Repositories.ModelsRepositories
{
    public class GroupRepository : BaseRepository<Group, int>, IGroupRepository
    {
        public GroupRepository(Microsoft.EntityFrameworkCore.DbContext context) : base(context) { }
    }
}
