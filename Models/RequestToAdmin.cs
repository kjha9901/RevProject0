using System;
using System.Collections.Generic;

namespace Project_0.Models;

public partial class RequestToAdmin
{
    public int RqNo { get; set; }

    public int? AccNo { get; set; }

    public string? RqType { get; set; }

    public string? RqPassword { get; set; }

    public bool? RqComplete { get; set; }

    public virtual CustomerUser? AccNoNavigation { get; set; }
}
