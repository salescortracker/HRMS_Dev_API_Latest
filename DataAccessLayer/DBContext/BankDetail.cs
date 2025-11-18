using System;
using System.Collections.Generic;

namespace DataAccessLayer.DBContext;

public partial class BankDetail
{
    public int BankDetailId { get; set; }

    public string BankName { get; set; } = null!;

    public string? BranchAddress { get; set; }

    public string AccountNumber { get; set; } = null!;

    public DateTime CreatedDate { get; set; }
}
