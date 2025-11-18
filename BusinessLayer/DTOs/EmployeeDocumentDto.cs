using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace BusinessLayer.DTOs
{
    public class EmployeeDocumentDto
    {
        public int? Id { get; set; }
        public int EmployeeId { get; set; }
        public int DocumentTypeId { get; set; }
        public string DocumentName { get; set; } = string.Empty;
        public string? DocumentNumber { get; set; }

        public DateOnly? IssuedDate { get; set; }
        public DateOnly? ExpiryDate { get; set; }

        public string? Remarks { get; set; }
        public bool IsConfidential { get; set; }

        public string? FilePath { get; set; }
        public IFormFile? DocumentFile { get; set; }

        public int? CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
      
        public DateTime? ModifiedDate { get; set; }
    }
}
