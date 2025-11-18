using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.DTOs;
using BusinessLayer.Interfaces;
using DataAccessLayer.DBContext;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.Implementations
{
    public class EmployeeImmigrationService : IEmployeeImmigrationService
    {
        private readonly HRMSContext _context;

        public EmployeeImmigrationService(HRMSContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<EmployeeImmigration>> GetAllAsync()
        {
            return await _context.EmployeeImmigrations
                .Select(x => new EmployeeImmigration
                {
                    ImmigrationId = x.ImmigrationId,
                    EmployeeId = x.EmployeeId,
                    FullName = x.FullName,
                    VisaType = x.VisaType,
                    VisaNumber = x.VisaNumber,
                    VisaIssueDate = x.VisaIssueDate,
                    VisaExpiryDate = x.VisaExpiryDate,
                    WorkAuthorizationStatus = x.WorkAuthorizationStatus
                })
                .ToListAsync();
        }

        public async Task<EmployeeImmigration> GetByIdAsync(int id)
        {
            var x = await _context.EmployeeImmigrations.FindAsync(id);
            if (x == null) return null;

            return new EmployeeImmigration
            {
                ImmigrationId = x.ImmigrationId,
                EmployeeId = x.EmployeeId,
                FullName = x.FullName,
                DateOfBirth = x.DateOfBirth,
                Nationality = x.Nationality,
                PassportNumber = x.PassportNumber,
                PassportExpiryDate = x.PassportExpiryDate,
                VisaType = x.VisaType,
                VisaNumber = x.VisaNumber,
                VisaIssueDate = x.VisaIssueDate,
                VisaExpiryDate = x.VisaExpiryDate,
                VisaIssuingCountry = x.VisaIssuingCountry,
                EmployerName = x.EmployerName,
                EmployerAddress = x.EmployerAddress,
                EmployerContact = x.EmployerContact,
                ContactPerson = x.ContactPerson,
                WorkAuthorizationStatus = x.WorkAuthorizationStatus,
                Remarks = x.Remarks
            };
        }

        public async Task<EmployeeImmigration> CreateAsync(EmployeeImmigration model)
        {
            var entity = new EmployeeImmigration
            {
                EmployeeId = model.EmployeeId,
                FullName = model.FullName,
                DateOfBirth = model.DateOfBirth,
                Nationality = model.Nationality,
                PassportNumber = model.PassportNumber,
                PassportExpiryDate = model.PassportExpiryDate,
                VisaType = model.VisaType,
                VisaNumber = model.VisaNumber,
                VisaIssueDate = model.VisaIssueDate,
                VisaExpiryDate = model.VisaExpiryDate,
                VisaIssuingCountry = model.VisaIssuingCountry,
                EmployerName = model.EmployerName,
                EmployerAddress = model.EmployerAddress,
                EmployerContact = model.EmployerContact,
                ContactPerson = model.ContactPerson,
                WorkAuthorizationStatus = model.WorkAuthorizationStatus,
                Remarks = model.Remarks,
                CreatedDate = DateTime.Now
            };

            _context.EmployeeImmigrations.Add(entity);
            await _context.SaveChangesAsync();

            model.ImmigrationId = entity.ImmigrationId;
            return model;
        }

        public async Task<EmployeeImmigration> UpdateAsync(EmployeeImmigration model)
        {
            var entity = await _context.EmployeeImmigrations.FindAsync(model.ImmigrationId);
            if (entity == null)
                throw new Exception("Record not found.");

            entity.FullName = model.FullName;
            entity.Nationality = model.Nationality;
            entity.PassportNumber = model.PassportNumber;
            entity.PassportExpiryDate = model.PassportExpiryDate;
            entity.VisaType = model.VisaType;
            entity.VisaNumber = model.VisaNumber;
            entity.VisaIssueDate = model.VisaIssueDate;
            entity.VisaExpiryDate = model.VisaExpiryDate;
            entity.VisaIssuingCountry = model.VisaIssuingCountry;
            entity.EmployerName = model.EmployerName;
            entity.EmployerAddress = model.EmployerAddress;
            entity.EmployerContact = model.EmployerContact;
            entity.ContactPerson = model.ContactPerson;
            entity.WorkAuthorizationStatus = model.WorkAuthorizationStatus;
            entity.Remarks = model.Remarks;
            entity.ModifiedDate = DateTime.Now;

            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.EmployeeImmigrations.FindAsync(id);
            if (entity == null)
                return false;

            _context.EmployeeImmigrations.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
