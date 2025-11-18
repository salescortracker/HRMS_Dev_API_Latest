using System;
using System.Collections.Generic;

namespace DataAccessLayer.Models;

public partial class UserForm
{
    public int Id { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Mobile { get; set; }

    public string? Address { get; set; }
}
