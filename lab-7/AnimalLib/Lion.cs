namespace AnimalLib;

[Comment("Lion class")]
public class Lion : Animal
{
    public Lion()
    {
        WhatAnimal = "Lion";
        HideFromOtherAnimals = false;
    }

    public override eFavoriteFood GetFavoriteFood()
    {
        return eFavoriteFood.Meat;
    }

    public override void SayHello()
    {
        Console.WriteLine("Hello, Im Lion!");
    }
}