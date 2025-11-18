


using Microsoft.AspNetCore.Http;

namespace BusinessLayer.DTOs
{
    public class EmployeeFormDto
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int RegionId { get; set; }
        public int CompanyId { get; set; }
        public int DocumentTypeId { get; set; }
        public string DocumentName { get; set; } = null!;
        public string EmployeeCode { get; set; } = null!;
        public DateOnly IssueDate { get; set; }
        public string FileName { get; set; } = null!;
        public string? Remarks { get; set; }
        public bool IsConfidential { get; set; }
        public int? CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }
        public string? FilePath { get; set; }

        // ✅ For Upload
        public IFormFile? DocumentFile { get; set; }


    }
}
