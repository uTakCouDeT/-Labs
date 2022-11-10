namespace AnimalLib;

[Comment("Pig class")]
public class Pig : Animal
{
    public Pig()
    {
        HideFromOtherAnimals = false;
        WhatAnimal = "Pig";
    }

    public override eFavoriteFood GetFavoriteFood()
    {
        return eFavoriteFood.Everything;
    }

    public override void SayHello()
    {
        Console.WriteLine("Hello, Im Pig!");
    }
}