using System;
using System.Collections.Generic;

namespace lab_10;

public partial class TodaysCondition
{
    public long id { get; set; }

    public long? tickerid { get; set; }

    public double? state { get; set; }

    public virtual Ticker? Ticker { get; set; }
}
