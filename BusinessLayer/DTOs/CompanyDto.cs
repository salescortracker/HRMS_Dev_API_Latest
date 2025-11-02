using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.DTOs
{
    public class CompanyDto
    {
        public int CompanyId { get; set; }
        public string CompanyName { get; set; } = null!;
        public string? CompanyCode { get; set; }
        public string? IndustryType { get; set; }
        public string? Headquarters { get; set; }
        public bool? IsActive { get; set; }
    }
}
