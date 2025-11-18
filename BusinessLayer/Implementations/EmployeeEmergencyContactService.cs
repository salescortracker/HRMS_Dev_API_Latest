using BusinessLayer.DTOs;
using BusinessLayer.Interfaces;
using DataAccessLayer.DBContext;
using DataAccessLayer.Repositories.GeneralRepository;


namespace BusinessLayer.Implementations
{
    public class EmployeeEmergencyContactService : IEmployeeEmergencyContactService
    {
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeEmergencyContactService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<EmployeeEmergencyContactDTO>> GetAllAsync()
        {
            var contacts = await _unitOfWork.Repository<EmployeeEmergencyContact>().GetAllAsync();
            return contacts.Select(c => MapToDto(c));
        }

        public async Task<IEnumerable<EmployeeEmergencyContactDTO>> GetByEmployeeIdAsync(int employeeId)
        {
            var contacts = await _unitOfWork.Repository<EmployeeEmergencyContact>()
                .FindAsync(c => c.EmployeeId == employeeId);

            return contacts.Select(c => MapToDto(c));
        }

        public async Task<EmployeeEmergencyContactDTO?> GetByIdAsync(int id)
        {
            var contact = await _unitOfWork.Repository<EmployeeEmergencyContact>().GetByIdAsync(id);
            return contact == null ? null : MapToDto(contact);
        }

        public async Task<EmployeeEmergencyContactDTO> AddAsync(EmployeeEmergencyContactDTO dto)
        {
            var entity = new EmployeeEmergencyContact
            {
                EmployeeId = dto.EmployeeId,
                ContactName = dto.ContactName,
                Relationship = dto.Relationship,
                PhoneNumber = dto.PhoneNumber,
                AlternatePhone = dto.AlternatePhone,
                Email = dto.Email,
                Address = dto.Address,
                CompanyId = dto.CompanyId,
                RegionId = dto.RegionId,
                CreatedBy = "System",
                CreatedDate = DateTime.Now
            };

            await _unitOfWork.Repository<EmployeeEmergencyContact>().AddAsync(entity);
            await _unitOfWork.CompleteAsync();

            return MapToDto(entity);
        }

        public async Task<EmployeeEmergencyContactDTO> UpdateAsync(EmployeeEmergencyContactDTO dto)
        {
            var entity = await _unitOfWork.Repository<EmployeeEmergencyContact>()
                .GetByIdAsync(dto.EmergencyContactId);

            if (entity == null)
                throw new Exception("Emergency contact not found.");

            entity.EmployeeId = dto.EmployeeId;
            entity.ContactName = dto.ContactName;
            entity.Relationship = dto.Relationship;
            entity.PhoneNumber = dto.PhoneNumber;
            entity.AlternatePhone = dto.AlternatePhone;
            entity.Email = dto.Email;
            entity.Address = dto.Address;
            entity.CompanyId = dto.CompanyId;
            entity.RegionId = dto.RegionId;
            entity.ModifiedBy = "System";
            entity.ModifiedDate = DateTime.Now;

            _unitOfWork.Repository<EmployeeEmergencyContact>().Update(entity);
            await _unitOfWork.CompleteAsync();

            return MapToDto(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _unitOfWork.Repository<EmployeeEmergencyContact>().GetByIdAsync(id);
            if (entity == null)
                return false;

            _unitOfWork.Repository<EmployeeEmergencyContact>().Remove(entity);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        // -----------------------------
        // Mapper: Entity → DTO
        // -----------------------------
        private EmployeeEmergencyContactDTO MapToDto(EmployeeEmergencyContact c)
        {
            return new EmployeeEmergencyContactDTO
            {
                EmergencyContactId = c.EmergencyContactId,
                EmployeeId = c.EmployeeId,
                ContactName = c.ContactName,
                Relationship = c.Relationship,
                PhoneNumber = c.PhoneNumber,
                AlternatePhone = c.AlternatePhone,
                Email = c.Email,
                Address = c.Address,
                CompanyId = c.CompanyId,
                RegionId = c.RegionId,
                CreatedBy = c.CreatedBy,
                CreatedDate = c.CreatedDate,
                ModifiedBy = c.ModifiedBy,
                ModifiedDate = c.ModifiedDate
            };
        }
    }
}
