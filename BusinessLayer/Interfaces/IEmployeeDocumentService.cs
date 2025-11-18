using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.DTOs;

namespace BusinessLayer.Interfaces
{
    public interface IEmployeeDocumentService
    {
        Task<IEnumerable<EmployeeDocumentDto>> GetDocumentsByEmployeeAsync(int employeeId);
        Task<EmployeeDocumentDto?> GetDocumentByIdAsync(int id);
        Task<int> AddDocumentAsync(EmployeeDocumentDto model);
        Task<bool> UpdateDocumentAsync(EmployeeDocumentDto model);
        Task<bool> DeleteDocumentAsync(int id);


        //dropdown for document type//
        Task<IEnumerable<DocumentTypeDto>> GetAllDocumentTypesAsync();

    }
}
