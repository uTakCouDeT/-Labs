namespace AnimalLib;

[Comment("Cow class")]
public class Cow : Animal
{
    public Cow()
    {
        WhatAnimal = "Cow";
        HideFromOtherAnimals = false;
    }

    public override eFavoriteFood GetFavoriteFood()
    {
        return eFavoriteFood.Plant;
    }

    public override void SayHello()
    {
        Console.WriteLine("Hello, Im Cow!");
    }
}