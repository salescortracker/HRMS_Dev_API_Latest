using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace BusinessLayer.DTOs
{
    public class EmployeeCertificationDto
    {
        public int CertificationId { get; set; }
        public int CompanyId { get; set; }
        public int RegionId { get; set; }
        public int EmployeeId { get; set; }

        public string CertificationName { get; set; } = string.Empty;
        public string CertificationType { get; set; } = string.Empty;
        public string? Description { get; set; }

        public string? DocumentPath { get; set; }
        public IFormFile? DocumentFile { get; set; }   // ✅ for file upload

        public int? CreatedBy { get; set; }            // ✅ nullable for ??= usage
        public DateTime CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
