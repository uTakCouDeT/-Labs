namespace AnimalLib;

[CommentAttibute("Pig class")]
class Pig : Animal
{
    public Pig()
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