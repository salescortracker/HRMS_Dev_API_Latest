using System;
using System.Collections.Generic;

namespace DataAccessLayer.Models;

public partial class BloodGroup
{
    public int BloodgroupId { get; set; }

    public string BloodGroupName { get; set; } = null!;

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public int? ModifiedBy { get; set; }
}
