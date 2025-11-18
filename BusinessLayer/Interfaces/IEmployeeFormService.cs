
using BusinessLayer.DTOs;

namespace BusinessLayer.Interfaces
{
    public interface IEmployeeFormService
    {

        Task<IEnumerable<EmployeeFormDto>> GetAllFormsAsync();
        Task<EmployeeFormDto?> GetFormByIdAsync(int id);
        Task<int> AddFormAsync(EmployeeFormDto dto);
        Task<bool> UpdateFormAsync(EmployeeFormDto dto);
        Task<bool> DeleteFormAsync(int id);
        Task<IEnumerable<DocumentTypeDto>> GetActiveDocumentTypesAsync();
    }
}
