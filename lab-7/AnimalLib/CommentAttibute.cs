namespace AnimalLib;

class CommentAttibute : Attribute
{
    public string Comment { get; }

    public CommentAttibute()
    {
    }

    public CommentAttibute(string comment) => Comment = comment;
}