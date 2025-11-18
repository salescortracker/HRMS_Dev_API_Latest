using BusinessLayer.DTOs;


namespace BusinessLayer.Interfaces
{
    public interface IEmployeeEmergencyContactService
    {
        Task<IEnumerable<EmployeeEmergencyContactDTO>> GetAllAsync();
        Task<IEnumerable<EmployeeEmergencyContactDTO>> GetByEmployeeIdAsync(int employeeId);
        Task<EmployeeEmergencyContactDTO?> GetByIdAsync(int id);
        Task<EmployeeEmergencyContactDTO> AddAsync(EmployeeEmergencyContactDTO contact);
        Task<EmployeeEmergencyContactDTO> UpdateAsync(EmployeeEmergencyContactDTO contact);
        Task<bool> DeleteAsync(int id);
    }
}
