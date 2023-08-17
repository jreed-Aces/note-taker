using System.Collections.Generic;

namespace note_taker.Services
{
    public interface INoteService
    {
        bool AddNote(string note);
        List<Note> PrintNotes();
    }
}

