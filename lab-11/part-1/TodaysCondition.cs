using System;
using System.Collections.Generic;

namespace lab_10;

public partial class TodaysCondition
{
    public long Id { get; set; }

    public long? TickerId { get; set; }

    public double? State { get; set; }

    public virtual Ticker? Ticker { get; set; }
}
