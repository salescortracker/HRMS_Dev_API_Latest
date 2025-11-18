using System;
using System.Collections.Generic;

namespace DataAccessLayer.Models;

public partial class AssetStatus
{
    public int AssetStatusId { get; set; }

    public string AssetStatusName { get; set; } = null!;

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int CreatedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public int ModifiedBy { get; set; }

    public bool? IsDefault { get; set; }
}
