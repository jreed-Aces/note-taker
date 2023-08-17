using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System.Linq;

namespace note_taker.Services
{
    public class NoteService : INoteService
    {
        public bool AddNote(string note)
        {
            List<Note> notes;
            var path = Directory.GetCurrentDirectory();
            if (File.Exists("notes.json"))
            {
                string json = File.ReadAllText("notes.json");
                notes = JsonConvert.DeserializeObject<List<Note>>(json);
            }
            else
            {
                notes = new List<Note>();
            }
            int highestId = notes.Count > 0 ? notes.Max(n => n.Id) : 0;
            Note newNote = new Note(highestId + 1, note);
            notes.Add(newNote);
            string newJson = JsonConvert.SerializeObject(notes);
            File.WriteAllText("notes.json", newJson);
            return true;
        }

        public List<Note> PrintNotes(bool all = false)
        {
            List<Note> notes = new List<Note>();
            if (File.Exists("notes.json"))
            {
                string json = File.ReadAllText("notes.json");
                notes = JsonConvert.DeserializeObject<List<Note>>(json);
                if (!all)
                {
                    notes = notes.Where(n => n.NoteStatus == Note.Status.Open).ToList();
                }
                notes.Sort((x, y) => DateTime.Compare(x.Timestamp, y.Timestamp));
            }
            return notes;
        }
    }
}

