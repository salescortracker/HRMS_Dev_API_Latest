using System;
using System.Collections.Generic;

namespace DataAccessLayer.DBContext;

public partial class DocumentType
{
    public int Id { get; set; }

    public string TypeName { get; set; } = null!;

    public virtual ICollection<EmployeeDocument> EmployeeDocuments { get; set; } = new List<EmployeeDocument>();
}
