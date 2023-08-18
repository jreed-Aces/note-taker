using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System.Linq;

namespace note_taker.Services
{
    public class NoteService : INoteService
    {
        private readonly IFileService _fileService;
        public NoteService(IFileService fileService)
        {
            _fileService = fileService;
        }
        
        public bool AddNote(string note)
        {
            List<Note> notes;
            var path = Directory.GetCurrentDirectory();
            if (File.Exists("notes.json"))
            {
                string json = _fileService.ReadAllText("notes.json");
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
            _fileService.WriteAllText("notes.json", newJson);
            return true;
        }

        public List<Note> PrintNotes(bool all = false)
        {
            List<Note> notes = new List<Note>();
            if (_fileService.Exists("notes.json"))
            {
                string json = _fileService.ReadAllText("notes.json");
                notes = JsonConvert.DeserializeObject<List<Note>>(json);
                if (!all)
                {
                    notes = notes.Where(n => n.NoteStatus == Note.Status.Open).ToList();
                }
                notes.Sort((x, y) => DateTime.Compare(x.Timestamp, y.Timestamp));
            }
            return notes;
        }

        public bool UpdateNoteStatus(int id, Note.Status status)
        {
            List<Note> notes;
            if (_fileService.Exists("notes.json"))
            {
                string json = _fileService.ReadAllText("notes.json");
                notes = JsonConvert.DeserializeObject<List<Note>>(json);
            }
            else
            {
                return false;
            }
            Note noteToUpdate = notes.Find(n => n.Id == id);
            if (noteToUpdate == null)
            {
                return false;
            }
            noteToUpdate.NoteStatus = status;
            string newJson = JsonConvert.SerializeObject(notes);
            _fileService.WriteAllText("notes.json", newJson);
            return true;
        }
    }
}

