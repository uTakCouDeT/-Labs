using System;
using System.Collections.Generic;

namespace lab_10;

public partial class Price
{
    public long Id { get; set; }

    public long? TickerId { get; set; }

    public double? Price1 { get; set; }

    public DateTime Date { get; set; }

    public virtual Ticker? Ticker { get; set; }
}
