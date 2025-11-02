using System;
using System.Collections.Generic;

namespace DataAccessLayer.Models;

public partial class Region
{
    public int RegionId { get; set; }

    public int CompanyId { get; set; }

    public string RegionName { get; set; } = null!;

    public string? Country { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public virtual Company Company { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
