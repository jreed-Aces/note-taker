namespace note_taker.Services
{
    public interface INoteService
    {
        void AddNote(string noteText);
        void PrintAllNotes();
    }
}

