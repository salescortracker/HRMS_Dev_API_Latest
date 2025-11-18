using System;
using System.Collections.Generic;

namespace DataAccessLayer.DBContext;

public partial class EmployeeDocument
{
    public int Id { get; set; }

    public int EmployeeId { get; set; }

    public int DocumentTypeId { get; set; }

    public string DocumentName { get; set; } = null!;

    public string? DocumentNumber { get; set; }

    public DateOnly IssuedDate { get; set; }

    public DateOnly? ExpiryDate { get; set; }

    public string FilePath { get; set; } = null!;

    public string? Remarks { get; set; }

    public bool IsConfidential { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual DocumentType DocumentType { get; set; } = null!;
}
