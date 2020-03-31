using DAL.Interfaces.ModelsAbstractRepositories;
using DAL.Models;


namespace DAL.Repositories.ModelsRepositories
{
    public class LogTypeRepository : BaseRepository<LogType, int>, ILogTypeRepository
    {
        public LogTypeRepository(Microsoft.EntityFrameworkCore.DbContext context) : base(context) { }
    }
}
