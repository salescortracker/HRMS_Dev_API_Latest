using System;
using System.Collections.Generic;

namespace DataAccessLayer.Models;

public partial class Smtpsetting
{
    public int Smtpid { get; set; }

    public string Host { get; set; } = null!;

    public int Port { get; set; }

    public string? Security { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? FromName { get; set; }

    public string? FromEmail { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public int? ModifiedBy { get; set; }
}
