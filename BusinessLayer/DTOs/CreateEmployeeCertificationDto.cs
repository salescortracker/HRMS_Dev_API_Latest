using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.DTOs
{
    public class CreateEmployeeCertificationDto
    {
        public int CompanyId { get; set; }
        public int RegionId { get; set; }
        public int EmployeeId { get; set; }
        public string CertificationName { get; set; } = null!;
        public string CertificationType { get; set; } = null!;
        public string? Description { get; set; }
        public string? DocumentPath { get; set; }
    }
}
