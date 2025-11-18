using System;
using System.Collections.Generic;

namespace DataAccessLayer.Models;

public partial class EmployeeHistory
{
    public int HistoryId { get; set; }

    public int EmployeeId { get; set; }

    public string Employer { get; set; } = null!;

    public string? JobTitle { get; set; }

    public DateOnly? TenureStart { get; set; }

    public DateOnly? TenureEnd { get; set; }

    public decimal? LastCtc { get; set; }

    public string? Website { get; set; }

    public string? EmployeeCode { get; set; }

    public string? ReasonForLeaving { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }
}
