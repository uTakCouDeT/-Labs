namespace AnimalLib;

public class All
{
    [CommentAttibute("Animal class")]
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

    [CommentAttibute("Animal classification enum")]
    public enum eClassificationAnimal
    {
        Herbivores,
        Carnivores,
        Omnivores
    }

    [CommentAttibute("Favorite food enum")]
    public enum eFavoriteFood
    {
        Meat,
        Plant,
        Everything
    }

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

    class CommentAttibute : Attribute
    {
        public string Comment { get; }

        public CommentAttibute()
        {
        }

        public CommentAttibute(string comment) => Comment = comment;
    }
}