using System;
using System.Collections.Generic;

namespace Project_0.Models;

public partial class CustomerUser
{
    public int AccNo { get; set; }

    public string? AccName { get; set; }

    public string? AccType { get; set; }

    public decimal? AccBalance { get; set; }

    public bool? AccIsActive { get; set; }

    public string? AccUsername { get; set; }

    public string? AccPassword { get; set; }

    public virtual ICollection<CustomerTransaction> CustomerTransactions { get; set; } = new List<CustomerTransaction>();
}
