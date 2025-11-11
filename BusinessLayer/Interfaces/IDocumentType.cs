using BusinessLayer.DTOs;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
   public interface IDocumentTypeService
    {
        Task<IEnumerable<CreateDocumentTypeDto>> GetAllDocumentTypeAsync();
        Task<CreateDocumentTypeDto?> GetDocumentTypeByIdAsync(int id);
        Task<IEnumerable<CreateDocumentTypeDto>> SearchDocumentTypeAsync(string? documentTypeName,bool? IsActive);
        Task<ResponseDto<CreateDocumentTypeDto>> CreateDocumentTypeAsync(CreateDocumentTypeDto document,int createdBy);
        Task<UpdateDocumentTypeDto?> UpdateDocumentTypeAsync(int id, UpdateDocumentTypeDto document,int modifiedBy);
        Task<bool> DeleteDocumentTypeAsync(int id);


        // Bulk 
        //Bulk Operation
        Task<(int insertedCount, int updatedCount)> BulkUploadAsync(IFormFile file, int createdBy);
    }
}
