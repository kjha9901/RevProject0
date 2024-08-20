using System;
using System.Collections.Generic;

namespace Project_0.Models;

public partial class CustomerTransaction
{
    public int TrNo { get; set; }

    public int? AccNo { get; set; }

    public string? TrType { get; set; }

    public decimal? TrAmount { get; set; }

    public int? TransferAcc { get; set; }

    public virtual CustomerUser? AccNoNavigation { get; set; }
}
