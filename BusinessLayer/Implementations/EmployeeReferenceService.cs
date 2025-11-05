
using BusinessLayer.DTOs;
using BusinessLayer.Interfaces;
using DataAccessLayer.DBContext;
using DataAccessLayer.Repositories.GeneralRepository;

namespace BusinessLayer.Implementations
{
    public class EmployeeReferenceService : IEmployeeReferenceService
    {
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeReferenceService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<EmployeeReferenceDto>> GetAllEmployeeReferencesAsync()
        {
            var references = await _unitOfWork.Repository<EmployeeReference>().GetAllAsync();
            return references.Select(r => MapToDto(r));
        }

        public async Task<EmployeeReferenceDto?> GetEmployeeReferenceByIdAsync(int id)
        {
            var reference = await _unitOfWork.Repository<EmployeeReference>().GetByIdAsync(id);
            return reference == null ? null : MapToDto(reference);
        }

        public async Task<EmployeeReferenceDto> AddEmployeeReferenceAsync(EmployeeReferenceDto dto)
        {
            var entity = new EmployeeReference
            {
                EmployeeId = dto.EmployeeId,
                RegionId = dto.RegionId,
                CompanyId = dto.CompanyId,
                Name = dto.Name,
                TitleOrDesignation = dto.TitleOrDesignation,
                CompanyName = dto.CompanyName,
                EmailId = dto.EmailId,
                MobileNumber = dto.MobileNumber,
                CreatedAt = DateTime.Now,
                CreatedBy = dto.CreatedBy
            };

            await _unitOfWork.Repository<EmployeeReference>().AddAsync(entity);
            await _unitOfWork.CompleteAsync();

            return MapToDto(entity);
        }

        public async Task<EmployeeReferenceDto> UpdateEmployeeReferenceAsync(int id, EmployeeReferenceDto dto)
        {
            var entity = await _unitOfWork.Repository<EmployeeReference>().GetByIdAsync(id);
            if (entity == null)
                throw new Exception("Employee Reference not found");

            entity.EmployeeId = dto.EmployeeId;
            entity.RegionId = dto.RegionId;
            entity.CompanyId = dto.CompanyId;
            entity.Name = dto.Name;
            entity.TitleOrDesignation = dto.TitleOrDesignation;
            entity.CompanyName = dto.CompanyName;
            entity.EmailId = dto.EmailId;
            entity.MobileNumber = dto.MobileNumber;
            entity.ModifiedAt = DateTime.Now;
            entity.ModifiedBy = dto.ModifiedBy;

            _unitOfWork.Repository<EmployeeReference>().Update(entity);
            await _unitOfWork.CompleteAsync();

            return MapToDto(entity);
        }

        public async Task<bool> DeleteEmployeeReferenceAsync(int id)
        {
            var entity = await _unitOfWork.Repository<EmployeeReference>().GetByIdAsync(id);
            if (entity == null) return false;

            _unitOfWork.Repository<EmployeeReference>().Remove(entity);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        private EmployeeReferenceDto MapToDto(EmployeeReference r)
        {
            return new EmployeeReferenceDto
            {
                ReferenceId = r.ReferenceId,
                EmployeeId = r.EmployeeId,
                RegionId = r.RegionId,
                CompanyId = r.CompanyId,
                Name = r.Name,
                TitleOrDesignation = r.TitleOrDesignation,
                CompanyName = r.CompanyName,
                EmailId = r.EmailId,
                MobileNumber = r.MobileNumber,
                CreatedAt = r.CreatedAt,
                CreatedBy = r.CreatedBy,
                ModifiedAt = r.ModifiedAt,
                ModifiedBy = r.ModifiedBy
            };
        }
    }
}
