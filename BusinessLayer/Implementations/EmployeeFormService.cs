
using BusinessLayer.DTOs;
using BusinessLayer.Interfaces;
using DataAccessLayer.DBContext;
using DataAccessLayer.Repositories.GeneralRepository;



namespace BusinessLayer.Implementations
{
    public class EmployeeFormService : IEmployeeFormService
    {
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeFormService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<EmployeeFormDto>> GetAllFormsAsync()
        {
            var forms = await _unitOfWork.Repository<EmployeeForm>().GetAllAsync();
            return forms.Select(MapToDto);
        }

        public async Task<EmployeeFormDto?> GetFormByIdAsync(int id)
        {
            var form = await _unitOfWork.Repository<EmployeeForm>().GetByIdAsync(id);
            return form == null ? null : MapToDto(form);
        }

        public async Task<int> AddFormAsync(EmployeeFormDto dto)
        {
            var entity = new EmployeeForm
            {
                EmployeeId = dto.EmployeeId,
                RegionId = dto.RegionId,
                CompanyId = dto.CompanyId,
                DocumentTypeId = dto.DocumentTypeId,
                DocumentName = dto.DocumentName,
                EmployeeCode = dto.EmployeeCode,
                IssueDate = dto.IssueDate,
                FileName = dto.FileName,
                Remarks = dto.Remarks,
                IsConfidential = dto.IsConfidential,
                CreatedBy = dto.CreatedBy,
                CreatedDate = DateTime.Now,
                FilePath = dto.FilePath
            };

            await _unitOfWork.Repository<EmployeeForm>().AddAsync(entity);
            await _unitOfWork.CompleteAsync();
            return entity.Id;
        }

        public async Task<bool> UpdateFormAsync(EmployeeFormDto dto)
        {
            var entity = await _unitOfWork.Repository<EmployeeForm>().GetByIdAsync(dto.Id);
            if (entity == null) return false;

            entity.DocumentName = dto.DocumentName;
            entity.DocumentTypeId = dto.DocumentTypeId;
            entity.EmployeeCode = dto.EmployeeCode;
            entity.FileName = dto.FileName;
            entity.Remarks = dto.Remarks;
            entity.IsConfidential = dto.IsConfidential;
            entity.FilePath = dto.FilePath ?? entity.FilePath;
            entity.ModifiedBy = dto.ModifiedBy;
            entity.ModifiedDate = DateTime.Now;

            _unitOfWork.Repository<EmployeeForm>().Update(entity);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<bool> DeleteFormAsync(int id)
        {
            var entity = await _unitOfWork.Repository<EmployeeForm>().GetByIdAsync(id);
            if (entity == null) return false;

            _unitOfWork.Repository<EmployeeForm>().Remove(entity);
            await _unitOfWork.CompleteAsync();
            return true;
        }
        public async Task<IEnumerable<DocumentTypeDto>> GetActiveDocumentTypesAsync()
        {
            var documentTypes = await _unitOfWork.Repository<DocumentType>().FindAsync(d => d.IsActive == true);

            return documentTypes.Select(d => new DocumentTypeDto
            {
                Id = d.Id,
                TypeName = d.TypeName,
                IsActive = d.IsActive
            });
        }



        private EmployeeFormDto MapToDto(EmployeeForm f)
        {
            return new EmployeeFormDto
            {
                Id = f.Id,
                EmployeeId = f.EmployeeId,
                RegionId = f.RegionId,
                CompanyId = f.CompanyId,
                DocumentTypeId = f.DocumentTypeId,
                DocumentName = f.DocumentName,
                EmployeeCode = f.EmployeeCode,
                IssueDate = f.IssueDate,
                FileName = f.FileName,
                Remarks = f.Remarks,
                IsConfidential = f.IsConfidential,
                CreatedBy = f.CreatedBy,
                ModifiedBy = f.ModifiedBy,
                FilePath = f.FilePath
            };
        }
    }
}
