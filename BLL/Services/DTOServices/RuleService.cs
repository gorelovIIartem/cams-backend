using AutoMapper;
using BLL.Infrastructure;
using BLL.Interfaces.DTOInterfaces;
using BLL.Models.DTOs;
using DAL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.DTOServices
{
    public class RuleService : IRuleService
    {
        private readonly IUnitOfWork _dataBase;
        private readonly IMapper _mapper;

        public RuleService(IUnitOfWork uow, IMapper mapper)
        {
            _dataBase = uow;
            _mapper = mapper;
        }

        public async Task<OperationDetails> CreateRule(RuleDTO ruleDTO)
        {
            if (ruleDTO == null)
                throw new ValidationException("There is no information about target rule.", "Empty input parameter.");
            if (await _dataBase.DeviceLogRepository.CheckIfExist(ruleDTO.RuleId))
                throw new ValidationException("Rule with this id already exists.", ruleDTO.RuleId.ToString());
            _dataBase.RuleRepository.Create(_mapper.Map<Rule>(ruleDTO));
            await _dataBase.SaveAsync();
            return new OperationDetails(true, "Rule created succesfully", ruleDTO.RuleId.ToString());
        }

        public async Task<OperationDetails> RemoveAllRulesByUserId(string userId)
        {
            if (await _dataBase.UserRepository.CheckIfExist(userId) == false)
                throw new ValidationException("There is no information about user.", $"Incorrect userId - {userId} ");
            ICollection<Rule> sortedRules = await _dataBase.RuleRepository.GetAllIncludingAsync(p => p.UserId == userId);
            _dataBase.RuleRepository.DeleteSeveral(sortedRules);
            return new OperationDetails(true, $"Rules for {userId} removed.", $"Target user - {userId}");
        }

        public async Task<OperationDetails> RemoveRule(int ruleId)
        {
            if (await _dataBase.RuleRepository.CheckIfExist(ruleId) == false)
                throw new ValidationException("There is no information about rule.", $"Incorrect ruleId - {ruleId.ToString()} ");
            Rule rule = await _dataBase.RuleRepository.GetByIdAsync(ruleId);
            _dataBase.RuleRepository.Delete(rule);
            return new OperationDetails(true, $"Rule is removed.", $"Target rule - {ruleId.ToString()}");
        }

        public async Task<OperationDetails> UpdateRule(RuleDTO ruleDTO)
        {
            if (await _dataBase.RuleRepository.CheckIfExist(ruleDTO.RuleId) == false)
                throw new ValidationException("There is no information about target rule.", $"Incorrect rule id - {ruleDTO.RuleId.ToString()}");
            _dataBase.RuleRepository.Update(_mapper.Map<Rule>(ruleDTO));
            await _dataBase.SaveAsync();
            return new OperationDetails(true, "Rule was updated.", $"Rule id - {ruleDTO.RuleId.ToString()}");
        }
    }
}
