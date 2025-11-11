using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace BusinessLayer.DTOs
{
    public class BulkUploadRequest
    {
        [Required]
        public IFormFile File { get; set; }

        public int? CreatedBy { get; set; } 
    }
}
