using System.Collections.Generic;

namespace note_taker.Services
{
    public interface INoteService
    {
        bool AddNote(string note);
        bool UpdateStatus(int id, Status status);
        List<Note> PrintNotes(bool all);
    }
}

