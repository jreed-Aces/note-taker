using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;

namespace note_taker.Services
{
    public class NoteService : INoteService
    {
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

        public void PrintAllNotes()
        {
            if (File.Exists("notes.json"))
            {
                string json = File.ReadAllText("notes.json");
                var notes = JsonConvert.DeserializeObject<List<Note>>(json);
                notes.Sort((x, y) => DateTime.Compare(x.Timestamp, y.Timestamp));
                foreach (var note in notes)
                {
                    Console.WriteLine($"{note.Id}: {note.Timestamp:G}: {note.Text}\n");
                }
            }
            else
            {
                Console.WriteLine("No notes found.");
            }
        }
    }
}

