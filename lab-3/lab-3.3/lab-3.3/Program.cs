namespace task3;

internal class Currency
{
    public double? Value { set; get; }

    public Currency(double? value)
    {
        Value = value;
    }
}

internal class CurrencyUSD : Currency
{
    public double? ToRUB { set; get; }
    public double? ToEUR { set; get; }

    public CurrencyUSD(double? value, double? toEur, double? toRub) : base(value)
    {
        ToRUB = toRub;
        ToEUR = toEur;
    }

    public static implicit operator CurrencyUSD(CurrencyEUR eur)
    {
        return new CurrencyUSD(eur.ToUSD * eur.Value, 1 / eur.ToUSD, eur.ToRUB / eur.ToUSD);
    }

    public static explicit operator CurrencyEUR(CurrencyUSD usd)
    {
        return new CurrencyEUR(usd.ToEUR * usd.Value, 1 / usd.ToEUR, usd.ToRUB / usd.ToEUR);
    }

    public static implicit operator CurrencyUSD(CurrencyRUB rub)
    {
        return new CurrencyUSD(rub.ToUSD * rub.Value, 1 / rub.ToUSD, rub.ToEUR / rub.ToUSD);
    }

    public static explicit operator CurrencyRUB(CurrencyUSD usd)
    {
        return new CurrencyRUB(usd.ToRUB * usd.Value, 1 / usd.ToRUB, usd.ToEUR / usd.ToRUB);
    }
}

internal class CurrencyEUR : Currency
{
    public double? ToRUB { set; get; }
    public double? ToUSD { set; get; }

    public CurrencyEUR(double? value, double? toRub, double? toUsd) : base(value)
    {
        ToRUB = toRub;
        ToUSD = toUsd;
    }

    public static explicit operator CurrencyRUB(CurrencyEUR eur)
    {
        return new CurrencyRUB(eur.ToRUB * eur.Value, 1 / eur.ToRUB, eur.ToUSD / eur.ToRUB);
    }

    public static implicit operator CurrencyEUR(CurrencyRUB rub)
    {
        return new CurrencyEUR(rub.ToEUR * rub.Value, 1 / rub.ToEUR, rub.ToUSD / rub.ToEUR);
    }
}

internal class CurrencyRUB : Currency
{
    public double? ToUSD { set; get; }
    public double? ToEUR { set; get; }

    public CurrencyRUB(double? value, double? toEur, double? toUsd) : base(value)
    {
        ToUSD = toUsd;
        ToEUR = toEur;
    }
}

internal class Program
{
    private static void Main()
    {
        // 0,017 0,0125
        // var eur = new CurrencyEUR(10, Convert.ToDouble(Console.ReadLine()), Convert.ToDouble(Console.ReadLine()));
        // var usd = new CurrencyUSD(50, Convert.ToDouble(Console.ReadLine()), Convert.ToDouble(Console.ReadLine()));
        var rub = new CurrencyRUB(5500, Convert.ToDouble(Console.ReadLine()), Convert.ToDouble(Console.ReadLine()));
        
        Console.WriteLine($"{rub.Value} рублей");
        var x = (CurrencyEUR)rub;
        Console.WriteLine($"{x.Value} долларов");   
        CurrencyUSD y = x;
        Console.WriteLine($"{y.Value} евро"); 
    }
}
