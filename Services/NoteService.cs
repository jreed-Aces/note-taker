using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using note_taker;

namespace note_taker.Services
{
    public class NoteService : INoteService
    {
        public List<Note> GetNotes()
        {
            List<Note> notes;
            if (File.Exists("notes.json"))
            {
                string json = File.ReadAllText("notes.json");
                notes = JsonConvert.DeserializeObject<List<Note>>(json);
                notes.Sort((x, y) => DateTime.Compare(x.Timestamp, y.Timestamp));
            }
            else
            {
                notes = new List<Note>();
            }
            return notes;
        }

        public void AddNote(string noteText)
        {
            List<Note> notes;
            int highestId = 0;
            if (File.Exists("notes.json"))
            {
                string json = File.ReadAllText("notes.json");
                notes = JsonConvert.DeserializeObject<List<Note>>(json);
                highestId = notes.Max(n => n.Id);
            }
            else
            {
                notes = new List<Note>();
            }
            Note newNote = new Note(highestId + 1, noteText);
            notes.Add(newNote);
            string newJson = JsonConvert.SerializeObject(notes);
            File.WriteAllText("notes.json", newJson);
        }
    }
}

