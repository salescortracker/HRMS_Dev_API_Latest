using System;
using System.Collections.Generic;

namespace DataAccessLayer.DBContext;

public partial class CertificationType
{
    public int CertificationTypeId { get; set; }

    public string CertificationTypeName { get; set; } = null!;

    public bool? IsActive { get; set; }
}
