using BusinessLayer.Interfaces;
using DataAccessLayer.DBContext;
using Microsoft.EntityFrameworkCore;


namespace BusinessLayer.Implementations
{
    public class EmployeeFamilyService : IEmployeeFamilyService
    {
        private readonly HRMSContext _context;

        public EmployeeFamilyService(HRMSContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<EmployeeFamilyDetail>> GetAllAsync()
        {
            return await _context.EmployeeFamilyDetails.ToListAsync();
        }

        public async Task<EmployeeFamilyDetail?> GetByIdAsync(int id)
        {
            return await _context.EmployeeFamilyDetails.FindAsync(id);
        }

        public async Task<EmployeeFamilyDetail> AddAsync(EmployeeFamilyDetail model)
        {
            _context.EmployeeFamilyDetails.Add(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<EmployeeFamilyDetail> UpdateAsync(EmployeeFamilyDetail model)
        {
            var existing = await _context.EmployeeFamilyDetails.FindAsync(model.FamilyId);
            if (existing == null)
                throw new KeyNotFoundException("Family record not found.");

            _context.Entry(existing).CurrentValues.SetValues(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var record = await _context.EmployeeFamilyDetails.FindAsync(id);
            if (record == null)
                return false;

            _context.EmployeeFamilyDetails.Remove(record);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
