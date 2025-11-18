using System;
using System.Collections.Generic;

namespace DataAccessLayer.Models;

public partial class MandatoryField
{
    public int Id { get; set; }

    public string ModuleName { get; set; } = null!;

    public string FieldName { get; set; } = null!;

    public bool IsRequired { get; set; }

    public DateTime? CreatedAt { get; set; }
}
