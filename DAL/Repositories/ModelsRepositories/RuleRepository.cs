using DAL.Interfaces.ModelsAbstractRepositories;
using DAL.Models;


namespace DAL.Repositories.ModelsRepositories
{
    public class RuleRepository : BaseRepository<Rule, int>, IRuleRepository
    {
        public RuleRepository(Microsoft.EntityFrameworkCore.DbContext context) : base(context) { }
    }
}
