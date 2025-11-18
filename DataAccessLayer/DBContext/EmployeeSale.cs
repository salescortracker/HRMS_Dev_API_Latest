using System;
using System.Collections.Generic;

namespace DataAccessLayer.DBContext;

public partial class EmployeeSale
{
    public int? EmployeeId { get; set; }

    public string? EmployeeName { get; set; }

    public string? Department { get; set; }

    public string? Region { get; set; }

    public decimal? SalesAmount { get; set; }

    public DateOnly? SalesDate { get; set; }
}
