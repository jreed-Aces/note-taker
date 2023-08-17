using System.Collections.Generic;
using note_taker;

namespace note_taker.Services
{
    public interface INoteService
    {
        List<Note> GetNotes();
        void AddNote(string noteText);
    }
}

