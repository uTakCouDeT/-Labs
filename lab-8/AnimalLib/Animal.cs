namespace AnimalLib;

[Comment("Animal class")]
public class Animal
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

    public virtual eClassificationAnimal GetClassificationAnimal()
    {
        return eClassificationAnimal.Omnivores;
    }

    public virtual eFavoriteFood GetFavoriteFood()
    {
        return eFavoriteFood.Everything;
    }

    public virtual void SayHello()
    {
        Console.WriteLine("Hello!");
    }
}