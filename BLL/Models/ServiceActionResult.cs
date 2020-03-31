using System.Collections.Generic;

namespace BLL.Models
{
    public class ServiceActionResult
    {
        public bool Success { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
