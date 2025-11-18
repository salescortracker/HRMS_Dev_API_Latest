using System;
using System.Collections.Generic;

namespace DataAccessLayer.DBContext;

public partial class EmployeeImmigration
{
    public int ImmigrationId { get; set; }

    public string EmployeeId { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public DateOnly? DateOfBirth { get; set; }

    public string? Nationality { get; set; }

    public string? PassportNumber { get; set; }

    public DateOnly? PassportExpiryDate { get; set; }

    public string? VisaType { get; set; }

    public string? VisaNumber { get; set; }

    public DateOnly? VisaIssueDate { get; set; }

    public DateOnly? VisaExpiryDate { get; set; }

    public string? VisaIssuingCountry { get; set; }

    public string? EmployerName { get; set; }

    public string? EmployerAddress { get; set; }

    public string? EmployerContact { get; set; }

    public string? ContactPerson { get; set; }

    public string? WorkAuthorizationStatus { get; set; }

    public string? Remarks { get; set; }

    public string? PassportCopyPath { get; set; }

    public string? VisaCopyPath { get; set; }

    public string? OtherDocumentsPath { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }
}
