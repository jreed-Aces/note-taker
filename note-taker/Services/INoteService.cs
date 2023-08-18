using System.Collections.Generic;

namespace note_taker.Services
{
    public interface INoteService
    {
        bool AddNote(string note);
        List<Note> PrintNotes(bool all = false);
        bool UpdateNoteStatus(int id, Note.Status status);
    }
}

