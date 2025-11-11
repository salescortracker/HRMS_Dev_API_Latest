using BusinessLayer.DTOs;
using BusinessLayer.Interfaces;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories.GeneralRepository;

namespace BusinessLayer.Implementations
{
    public class CompanyService : ICompanyService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CompanyService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<CompanyDto>> GetAllCompaniesAsync()
        {
            var companies = await _unitOfWork.Repository<Company>().GetAllAsync();
            return companies.Select(c => MapToDto(c));
        }

        public async Task<CompanyDto?> GetCompanyByIdAsync(int id)
        {
            var company = await _unitOfWork.Repository<Company>().GetByIdAsync(id);
            return company == null ? null : MapToDto(company);
        }

        public async Task<IEnumerable<CompanyDto>> SearchCompaniesAsync(object filter)
        {
            // Dynamic search: convert object to dictionary
            var props = filter.GetType().GetProperties();
            var allCompanies = await _unitOfWork.Repository<Company>().GetAllAsync();
            var query = allCompanies.AsQueryable();

            foreach (var prop in props)
            {
                var name = prop.Name;
                var value = prop.GetValue(filter);

                if (value != null)
                {
                    switch (name)
                    {
                        case nameof(Company.CompanyName):
                            query = query.Where(c => c.CompanyName != null && c.CompanyName.Contains(value.ToString()!));
                            break;
                        case nameof(Company.CompanyCode):
                            query = query.Where(c => c.CompanyCode == value.ToString());
                            break;
                        case nameof(Company.IsActive):
                            bool isActive = Convert.ToBoolean(value);
                            query = query.Where(c => c.IsActive == isActive);
                            break;
                    }
                }
            }

            var results = query.ToList();
            return results.Select(c => MapToDto(c));
        }

        public async Task<CompanyDto> AddCompanyAsync(CompanyDto dto)
        {
            var entity = new Company
            {
                CompanyName = dto.CompanyName,
                CompanyCode = dto.CompanyCode,
                IndustryType = dto.IndustryType,
                Headquarters = dto.Headquarters,
                IsActive = dto.IsActive,
                CreatedDate = DateTime.Now
            };

            await _unitOfWork.Repository<Company>().AddAsync(entity);
            await _unitOfWork.CompleteAsync();

            return MapToDto(entity);
        }

        public async Task<CompanyDto> UpdateCompanyAsync(int id, CompanyDto dto)
        {
            var entity = await _unitOfWork.Repository<Company>().GetByIdAsync(id);
            if (entity == null) throw new Exception("Company not found");

            entity.CompanyName = dto.CompanyName;
            entity.CompanyCode = dto.CompanyCode;
            entity.IndustryType = dto.IndustryType;
            entity.Headquarters = dto.Headquarters;
            entity.IsActive = dto.IsActive;
            entity.ModifiedAt = DateTime.Now;

            _unitOfWork.Repository<Company>().Update(entity);
            await _unitOfWork.CompleteAsync();

            return MapToDto(entity);
        }

        public async Task<bool> DeleteCompanyAsync(int id)
        {
            var entity = await _unitOfWork.Repository<Company>().GetByIdAsync(id);
            if (entity == null) return false;

            _unitOfWork.Repository<Company>().Remove(entity);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        // Simple mapper
        private CompanyDto MapToDto(Company c)
        {
            return new CompanyDto
            {
                CompanyId = c.CompanyId,
                CompanyName = c.CompanyName,
                CompanyCode = c.CompanyCode,
                IndustryType = c.IndustryType,
                Headquarters = c.Headquarters,
                IsActive = c.IsActive
            };
        }
    }
}
