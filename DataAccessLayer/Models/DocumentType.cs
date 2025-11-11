using System;
using System.Collections.Generic;

namespace DataAccessLayer.Models;

public partial class DocumentType
{
    public int DocumentTypeId { get; set; }

    public string DocumentTypeName { get; set; } = null!;

    public bool? IsActive { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }
}
