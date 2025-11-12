using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.DTOs;
using BusinessLayer.Implementations;

namespace BusinessLayer.Interfaces
{
    public interface IEmployeeCertificationService
    {
        Task<int> CreateCertificationAsync(EmployeeCertificationDto dto);
        Task<bool> UpdateCertificationAsync(int certificationId, EmployeeCertificationDto dto);
        Task<bool> DeleteCertificationAsync(int certificationId);
        Task<EmployeeCertificationDto?> GetCertificationByIdAsync(int certificationId);
        Task<List<EmployeeCertificationDto>> GetEmployeeCertificationsAsync(int employeeId);

        //dropdown for certification type//
        Task<List<CertificationTypeDto>> GetCertificationTypesAsync();
    }
}
