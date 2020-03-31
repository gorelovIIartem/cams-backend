using BLL.Infrastructure;
using BLL.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces.DTOInterfaces
{
    public interface IRuleService
    {
        Task<OperationDetails> CreateRule(RuleDTO ruleDTO);
        Task<OperationDetails> RemoveRule(int ruleId);
        Task<OperationDetails> RemoveAllRulesByUserId(string userId);
        Task<OperationDetails> UpdateRule(RuleDTO ruleDTO);
    }
}
