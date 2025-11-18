using System;
using System.Collections.Generic;

namespace DataAccessLayer.DBContext;

public partial class ModeOfStudy
{
    public int ModeOfStudyId { get; set; }

    public string ModeName { get; set; } = null!;

    public bool IsActive { get; set; }

    public DateTime CreatedDate { get; set; }

    public virtual ICollection<EmployeeEducation> EmployeeEducations { get; set; } = new List<EmployeeEducation>();
}
