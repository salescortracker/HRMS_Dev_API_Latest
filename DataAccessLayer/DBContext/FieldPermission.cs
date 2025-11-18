using System;
using System.Collections.Generic;

namespace DataAccessLayer.DBContext;

public partial class FieldPermission
{
    public int Id { get; set; }

    public bool FirstNameRequired { get; set; }

    public bool LastNameRequired { get; set; }

    public bool MobileRequired { get; set; }

    public bool AddressRequired { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
