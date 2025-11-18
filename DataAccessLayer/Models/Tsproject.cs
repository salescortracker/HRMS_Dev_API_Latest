using System;
using System.Collections.Generic;

namespace DataAccessLayer.Models;

public partial class Tsproject
{
    public int ProjectId { get; set; }

    public string ProjectName { get; set; } = null!;

    public string ProjectCode { get; set; } = null!;

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public int? ProjectManagerId { get; set; }

    public bool? Billable { get; set; }

    public decimal? HourlyRate { get; set; }

    public string? Status { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public int? ModifiedBy { get; set; }
}
