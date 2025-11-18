using System;
using System.Collections.Generic;

namespace DataAccessLayer.Models;

public partial class JobHistory
{
    public int JobHistoryId { get; set; }

    public string Employer { get; set; } = null!;

    public string? JobTitle { get; set; }

    public DateOnly? TenureFrom { get; set; }

    public DateOnly? TenureTo { get; set; }

    public decimal? LastCtc { get; set; }

    public string? DocumentPath { get; set; }

    public DateTime CreatedDate { get; set; }

    public string Website { get; set; } = null!;

    public string EmpCode { get; set; } = null!;

    public string Reason { get; set; } = null!;
}
