namespace AnimalLib;

[CommentAttibute("Cow class")]
class Cow : Animal
{
    public Cow()
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