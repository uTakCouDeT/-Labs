using System;

namespace lab_1._1
{
    internal class Program
    { 
        public static void Main(string[] args)
        {
            Console.WriteLine($" Bool: \n\t MinValue: {bool.FalseString} \n\t MaxValue: {bool.TrueString} ");
            Console.WriteLine($" Byte: \n\t MinValue: {byte.MinValue} \n\t MaxValue: {byte.MaxValue} ");
            Console.WriteLine($" SByte: \n\t MinValue: {sbyte.MinValue} \n\t MaxValue: {sbyte.MaxValue} ");
            Console.WriteLine($" Char: \n\t MinValue: {char.MinValue} \n\t MaxValue: {char.MaxValue} ");
            Console.WriteLine($" Short: \n\t MinValue: {short.MinValue} \n\t MaxValue: {short.MaxValue} ");
            Console.WriteLine($" UShort: \n\t MinValue: {ushort.MinValue} \n\t MaxValue: {ushort.MaxValue} ");
            Console.WriteLine($" Int: \n\t MinValue: {int.MinValue} \n\t MaxValue: {int.MaxValue} ");
            Console.WriteLine($" UInt: \n\t MinValue: {uint.MinValue} \n\t MaxValue: {uint.MaxValue} ");
            Console.WriteLine($" Long: \n\t MinValue: {long.MinValue} \n\t MaxValue: {long.MaxValue} ");
            Console.WriteLine($" ULong: \n\t MinValue: {ulong.MinValue} \n\t MaxValue: {ulong.MaxValue} ");
            Console.WriteLine($" Decimal: \n\t MinValue: {decimal.MinValue} \n\t MaxValue: {decimal.MaxValue} ");
            Console.WriteLine($" Float: \n\t MinValue: {float.MinValue} \n\t MaxValue: {float.MaxValue} ");
            Console.WriteLine($" Double: \n\t MinValue: {double.MinValue} \n\t MaxValue: {double.MaxValue} ");
        }
    }
}