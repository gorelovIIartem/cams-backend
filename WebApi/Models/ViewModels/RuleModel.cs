using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models.ViewModels
{
    public class RuleModel
    {
        public int RuleId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Metadata { get; set; }
        public string UserId { get; set; }
    }
}
