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
    public  class EmployeeDocumentService : IEmployeeDocumentService
    {
        private readonly HRMSContext _context;

        public EmployeeDocumentService(HRMSContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<EmployeeDocumentDto>> GetDocumentsByEmployeeAsync(int employeeId)
        {
            var docs = await _context.EmployeeDocuments
                .Where(d => d.EmployeeId == employeeId)
                .OrderByDescending(d => d.CreatedDate)
                .ToListAsync();

            return docs.Select(MapToDto);
        }

        public async Task<EmployeeDocumentDto?> GetDocumentByIdAsync(int id)
        {
            var doc = await _context.EmployeeDocuments.FirstOrDefaultAsync(d => d.Id == id);
            return doc == null ? null : MapToDto(doc);
        }

        public async Task<int> AddDocumentAsync(EmployeeDocumentDto model)
        {
            var entity = new EmployeeDocument
            {
                EmployeeId = model.EmployeeId,
                DocumentTypeId = model.DocumentTypeId,
                DocumentName = model.DocumentName,
                DocumentNumber = model.DocumentNumber,
                IssuedDate = model.IssuedDate ?? DateOnly.FromDateTime(DateTime.Now),
                ExpiryDate = model.ExpiryDate,
                FilePath = model.FilePath ?? string.Empty,
                Remarks = model.Remarks,
                IsConfidential = model.IsConfidential,
                CreatedBy = model.CreatedBy ?? 0, // ✅ fixed
                CreatedDate = DateTime.Now
            };

            _context.EmployeeDocuments.Add(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<bool> UpdateDocumentAsync(EmployeeDocumentDto model)
        {
            var entity = await _context.EmployeeDocuments.FirstOrDefaultAsync(d => d.Id == model.Id);
            if (entity == null)
                return false;

            entity.DocumentTypeId = model.DocumentTypeId;
            entity.DocumentName = model.DocumentName;
            entity.DocumentNumber = model.DocumentNumber;
            entity.IssuedDate = model.IssuedDate ?? entity.IssuedDate;
            entity.ExpiryDate = model.ExpiryDate;
            entity.FilePath = model.FilePath ?? entity.FilePath;
            entity.Remarks = model.Remarks;
            entity.IsConfidential = model.IsConfidential;
            entity.ModifiedBy = model.ModifiedBy ?? 0; // ✅ fixed
            entity.ModifiedDate = DateTime.Now;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteDocumentAsync(int id)
        {
            var entity = await _context.EmployeeDocuments.FirstOrDefaultAsync(d => d.Id == id);
            if (entity == null)
                return false;

            _context.EmployeeDocuments.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        private static EmployeeDocumentDto MapToDto(EmployeeDocument e)
        {
            return new EmployeeDocumentDto
            {
                Id = e.Id,
                EmployeeId = e.EmployeeId,
                DocumentTypeId = e.DocumentTypeId,
                DocumentName = e.DocumentName,
                DocumentNumber = e.DocumentNumber,
                IssuedDate = e.IssuedDate,
                ExpiryDate = e.ExpiryDate,
                FilePath = e.FilePath,
                Remarks = e.Remarks,
                IsConfidential = e.IsConfidential,
                CreatedBy = e.CreatedBy,
                CreatedDate = e.CreatedDate,
                ModifiedBy = e.ModifiedBy,
                ModifiedDate = e.ModifiedDate
            };
        }
        public async Task<IEnumerable<DocumentTypeDto>> GetAllDocumentTypesAsync()
        {
            var documentTypes = await _context.DocumentTypes
                .OrderBy(t => t.TypeName)
                .Select(t => new DocumentTypeDto
                {
                    Id = t.Id,
                    TypeName = t.TypeName
                })
                .ToListAsync();

            return documentTypes;
        }

    }
}
