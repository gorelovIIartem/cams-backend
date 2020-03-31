using AutoMapper;
using BLL.Interfaces.DTOInterfaces;
using BLL.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApi.Models.ViewModels;

namespace WebApi.Controllers
{
    public class RuleController : ControllerBase
    {
        private readonly IRuleService _ruleService;
        private readonly IMapper _mapper;

        public RuleController(IRuleService ruleService, IMapper mapper)
        {
            _ruleService = ruleService;
            _mapper = mapper;
        }

        [HttpPut]
        public async Task<IActionResult> CreateNewRule([FromBody]RuleModel ruleModel)
        {
            RuleDTO ruleDTO = _mapper.Map<RuleDTO>(ruleModel);
            var operationDetails = await _ruleService.CreateRule(ruleDTO);
            return Ok(operationDetails);
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveAllRulesByUserId(string userId)
        {
            var operationDetails = await _ruleService.RemoveAllRulesByUserId(userId);
            return Ok(operationDetails);
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveRuleById(int ruleId)
        {
            var operationDetails = await _ruleService.RemoveRule(ruleId);
            return Ok(operationDetails);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateRule([FromBody] RuleModel ruleModel)
        {
            var operationDetails = await _ruleService.UpdateRule(_mapper.Map<RuleDTO>(ruleModel));
            return Ok(operationDetails);
        }
    }
}