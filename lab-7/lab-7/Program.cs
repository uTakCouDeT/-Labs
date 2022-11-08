using System.Globalization;
using System.Linq;
using System.Net;

abstract class Animal
{
    public string Country { set; get; }
    public bool HideFromOtherAnimals { set; get; }
    public string Name { set; get; }
    public string WhatAnimal { set; get; }

    public Animal()
    {
        Country = "";
        HideFromOtherAnimals = true;
        Name = "";
        WhatAnimal = "";
    }

    public Animal(string contry, bool hideFromOtherAnimals, string name, string whatAnimal)
    {
        Country = contry;
        HideFromOtherAnimals = hideFromOtherAnimals;
        Name = name;
        WhatAnimal = whatAnimal;
    }

    public void Deconstruct()
    {
        Country = "";
        HideFromOtherAnimals = true;
        Name = "";
        WhatAnimal = "";
    }

    public eClassificationAnimal GetClassificationAnimal()
    {
        return eClassificationAnimal.Omnivores;
    }

    public eFavoriteFood GetFavoriteFood()
    {
        return eFavoriteFood.Everything;
    }

    public void SayHello()
    {
        Console.WriteLine("Hello!");
    }
}

public enum eClassificationAnimal
{
    Herbivores,
    Carnivores,
    Omnivores
}

public enum eFavoriteFood
{
    Meat,
    Plant,
    Everything
}

class Cow : Animal
{
    Cow()
    {
        WhatAnimal = "Cow";
        HideFromOtherAnimals = false;
    }

    public eFavoriteFood GetFavoriteFood()
    {
        return eFavoriteFood.Plant;
    }

    public void SayHello()
    {
        Console.WriteLine("Hello, Im Cow!");
    }
}

class Lion : Animal
{
    Lion()
    {
        WhatAnimal = "Lion";
        HideFromOtherAnimals = false;
    }

    public eFavoriteFood GetFavoriteFood()
    {
        return eFavoriteFood.Meat;
    }

    public void SayHello()
    {
        Console.WriteLine("Hello, Im Lion!");
    }
}

class Pig : Animal
{
    Pig()
    {
        HideFromOtherAnimals = false;
        WhatAnimal = "Pig";
    }

    public eFavoriteFood GetFavoriteFood()
    {
        return eFavoriteFood.Everything;
    }

    public void SayHello()
    {
        Console.WriteLine("Hello, Im Pig!");
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello");
    }
}