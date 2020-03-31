using DAL.Interfaces.ModelsAbstractRepositories;
using DAL.Models;


namespace DAL.Repositories.ModelsRepositories
{
    public class UserRepository : BaseRepository<User, string>, IUserRepository
    {
        public UserRepository(Microsoft.EntityFrameworkCore.DbContext context) : base(context) { }
    }
}
