
using BusinessLayer.DTOs;

namespace BusinessLayer.Interfaces
{
    public interface IEmployeeEducationService
    {
        Task<IEnumerable<EmployeeEducationDto>> GetByEmployeeIdAsync(int employeeId);
        Task<EmployeeEducationDto?> GetByIdAsync(int educationId);
        Task<int> AddAsync(EmployeeEducationDto model);
        Task<bool> UpdateAsync(EmployeeEducationDto model);
        Task<bool> DeleteAsync(int educationId);

        //drop for mode of study//
        Task<IEnumerable<ModeOfStudyDto>> GetModeOfStudyListAsync();

    }
}
