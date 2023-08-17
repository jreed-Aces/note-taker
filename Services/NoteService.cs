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
            // existing code...
        }

        public bool UpdateStatus(int id, Status status)
        {
            List<Note> notes;
            if (File.Exists("notes.json"))
            {
                string json = File.ReadAllText("notes.json");
                notes = JsonConvert.DeserializeObject<List<Note>>(json);
                var note = notes.FirstOrDefault(n => n.Id == id);
                if (note != null)
                {
                    note.NoteStatus = status;
                    string newJson = JsonConvert.SerializeObject(notes);
                    File.WriteAllText("notes.json", newJson);
                    return true;
                }
            }
            return false;
        }

        public List<Note> PrintNotes(bool all)
        {
            List<Note> notes = new List<Note>();
            if (File.Exists("notes.json"))
            {
                string json = File.ReadAllText("notes.json");
                notes = JsonConvert.DeserializeObject<List<Note>>(json);
                notes.Sort((x, y) => DateTime.Compare(x.Timestamp, y.Timestamp));
                if (!all)
                {
                    notes = notes.Where(n => n.NoteStatus == Status.Open).ToList();
                }
            }
            return notes;
        }
    }
}
                notes = new List<Note>();
            }
            int highestId = notes.Count > 0 ? notes.Max(n => n.Id) : 0;
            Note newNote = new Note(highestId + 1, note);
            notes.Add(newNote);
            string newJson = JsonConvert.SerializeObject(notes);
            File.WriteAllText("notes.json", newJson);
            return true;
        }

        public List<Note> PrintNotes()
        {
            List<Note> notes = new List<Note>();
            if (File.Exists("notes.json"))
            {
                string json = File.ReadAllText("notes.json");
                notes = JsonConvert.DeserializeObject<List<Note>>(json);
                notes.Sort((x, y) => DateTime.Compare(x.Timestamp, y.Timestamp));
            }
            return notes;
        }
    }
}

