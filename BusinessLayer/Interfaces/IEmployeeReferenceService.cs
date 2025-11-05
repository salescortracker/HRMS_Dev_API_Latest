
using BusinessLayer.DTOs;

namespace BusinessLayer.Interfaces
{
    public interface IEmployeeReferenceService
    {
        Task<IEnumerable<EmployeeReferenceDto>> GetAllEmployeeReferencesAsync();
        Task<EmployeeReferenceDto?> GetEmployeeReferenceByIdAsync(int id);
        Task<EmployeeReferenceDto> AddEmployeeReferenceAsync(EmployeeReferenceDto dto);
        Task<EmployeeReferenceDto> UpdateEmployeeReferenceAsync(int id, EmployeeReferenceDto dto);
        Task<bool> DeleteEmployeeReferenceAsync(int id);
    }
}
