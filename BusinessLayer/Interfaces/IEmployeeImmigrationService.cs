using BusinessLayer.DTOs;
using DataAccessLayer.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IEmployeeImmigrationService
    {
        Task<IEnumerable<EmployeeImmigration>> GetAllAsync();
        Task<EmployeeImmigration> GetByIdAsync(int id);
        Task<EmployeeImmigration> CreateAsync(EmployeeImmigration model);
        Task<EmployeeImmigration> UpdateAsync(EmployeeImmigration model);
        Task<bool> DeleteAsync(int id);
    }
}
