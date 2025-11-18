using DataAccessLayer.DBContext;


namespace BusinessLayer.Interfaces
{
    public interface IEmployeeFamilyService
    {
        Task<IEnumerable<EmployeeFamilyDetail>> GetAllAsync();
        Task<EmployeeFamilyDetail?> GetByIdAsync(int id);
        Task<EmployeeFamilyDetail> AddAsync(EmployeeFamilyDetail model);
        Task<EmployeeFamilyDetail> UpdateAsync(EmployeeFamilyDetail model);
        Task<bool> DeleteAsync(int id);
    }
}
