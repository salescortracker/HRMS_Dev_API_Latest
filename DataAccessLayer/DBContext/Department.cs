using System;
using System.Collections.Generic;

namespace DataAccessLayer.DBContext;

public partial class Department
{
    public int DepartmentId { get; set; }

    public string DepartmentName { get; set; } = null!;

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int CreatedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public int ModifiedBy { get; set; }

    public bool IsDeleted { get; set; }

    public bool IsDefault { get; set; }
}
