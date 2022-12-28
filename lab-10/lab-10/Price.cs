using System;
using System.Collections.Generic;

namespace lab_10;

public partial class Price
{
    public long id { get; set; }

    public long? tickerid { get; set; }

    public double? price { get; set; }

    public string? date { get; set; }

    public virtual Ticker? Ticker { get; set; }
}
