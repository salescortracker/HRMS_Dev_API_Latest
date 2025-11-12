using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.DTOs;
using BusinessLayer.Interfaces;
using DataAccessLayer.DBContext;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.Implementations
{
    public class EmployeeCertificationService : IEmployeeCertificationService
    {
        private readonly HRMSContext _context;

        public EmployeeCertificationService(HRMSContext context)
        {
            _context = context;
        }

        public async Task<int> CreateCertificationAsync(EmployeeCertificationDto dto)
        {
            var entity = new EmployeeCertification
            {
                CompanyId = dto.CompanyId,
                RegionId = dto.RegionId,
                EmployeeId = dto.EmployeeId,
                CertificationName = dto.CertificationName,
                CertificationType = dto.CertificationType,
                Description = dto.Description,
                DocumentPath = dto.DocumentPath,
                CreatedBy = dto.CreatedBy ?? 0, // ✅ fix
                CreatedDate = DateTime.Now,
                IsActive = true
            };

            await _context.EmployeeCertifications.AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity.CertificationId;
        }

        public async Task<EmployeeCertificationDto?> GetCertificationByIdAsync(int certificationId)
        {
            return await _context.EmployeeCertifications
                .Where(x => x.CertificationId == certificationId && x.IsActive == true)
                .Select(x => new EmployeeCertificationDto
                {
                    CertificationId = x.CertificationId,
                    CompanyId = x.CompanyId,
                    RegionId = x.RegionId,
                    EmployeeId = x.EmployeeId,
                    CertificationName = x.CertificationName,
                    CertificationType = x.CertificationType,
                    Description = x.Description,
                    DocumentPath = x.DocumentPath,
                    CreatedDate = x.CreatedDate
                })
                .FirstOrDefaultAsync();
        }

        public async Task<List<EmployeeCertificationDto>> GetEmployeeCertificationsAsync(int employeeId)
        {
            return await _context.EmployeeCertifications
                .Where(x => x.EmployeeId == employeeId && x.IsActive == true)
                .Select(x => new EmployeeCertificationDto
                {
                    CertificationId = x.CertificationId,
                    CompanyId = x.CompanyId,
                    RegionId = x.RegionId,
                    EmployeeId = x.EmployeeId,
                    CertificationName = x.CertificationName,
                    CertificationType = x.CertificationType,
                    Description = x.Description,
                    DocumentPath = x.DocumentPath,
                    CreatedDate = x.CreatedDate
                })
                .OrderByDescending(x => x.CreatedDate)
                .ToListAsync();
        }

        public async Task<bool> UpdateCertificationAsync(int certificationId, EmployeeCertificationDto dto)
        {
            var entity = await _context.EmployeeCertifications.FindAsync(certificationId);
            if (entity == null)
                return false;

            entity.CertificationName = dto.CertificationName;
            entity.CertificationType = dto.CertificationType;
            entity.Description = dto.Description;
            entity.DocumentPath = dto.DocumentPath;
            entity.ModifiedBy = dto.ModifiedBy ?? 0;  // ✅ fix
            entity.ModifiedDate = DateTime.Now;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteCertificationAsync(int certificationId)
        {
            var entity = await _context.EmployeeCertifications.FirstOrDefaultAsync(x => x.CertificationId == certificationId);
            if (entity == null)
                return false;

            entity.IsActive = false;
            entity.ModifiedDate = DateTime.Now;

            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<List<CertificationTypeDto>> GetCertificationTypesAsync()
        {
            return await _context.CertificationTypes
                .Where(x => x.IsActive == true)
                .Select(x => new CertificationTypeDto
                {
                    CertificationTypeId = x.CertificationTypeId,
                    CertificationTypeName = x.CertificationTypeName
                })
                .OrderBy(x => x.CertificationTypeName)
                .ToListAsync();
        }

    }
}
