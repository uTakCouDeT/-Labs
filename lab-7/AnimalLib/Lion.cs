namespace AnimalLib;

[CommentAttibute("Lion class")]
class Lion : Animal
{
    public Lion()
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