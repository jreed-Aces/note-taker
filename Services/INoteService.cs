using System.Collections.Generic;

namespace note_taker.Services
{
    public interface INoteService
    {
        bool AddNote(int id, string note);
        List<Note> PrintNotes();
    }
}

