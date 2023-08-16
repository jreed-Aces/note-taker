using System;

public class Note
{
    public int Id { get; private set; }
    public string Text { get; private set; }
    public DateTime Timestamp { get; private set; }

    public Note(int id, string text)
    {
        Id = id;
        Text = text;
        Timestamp = DateTime.Now;
    }
}

