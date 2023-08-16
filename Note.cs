using System;

public class Note
{
    public Guid Id { get; private set; }
    public string Text { get; private set; }
    public DateTime Timestamp { get; private set; }

    public Note(string text)
    {
        Id = Guid.NewGuid();
        Text = text;
        Timestamp = DateTime.Now;
    }
}

