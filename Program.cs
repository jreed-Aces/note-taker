using System.CommandLine;
using System.CommandLine.Invocation;
using Newtonsoft.Json;
using System.IO;

// See https://aka.ms/new-console-template for more information
static void Main(string[] args)
{
    var rootCommand = new RootCommand
    {
        new Option<string>(
            "--note",
            getDefaultValue: () => "No note provided")
    };

    rootCommand.Handler = CommandHandler.Create<string>((note) =>
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
        Note newNote = new Note(highestId + 1, note);
        notes.Add(newNote);
        string newJson = JsonConvert.SerializeObject(notes);
        File.WriteAllText("notes.json", newJson);
    });

    var printCommand = new Command("print");
    printCommand.Handler = CommandHandler.Create(() =>
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
    });
    rootCommand.Add(printCommand);
    var updateStatusCommand = new Command("updateStatus")
    {
        new Argument<int>("id"),
        new Argument<string>("status")
    };
    updateStatusCommand.Handler = CommandHandler.Create<int, string>((id, status) =>
    {
        if (File.Exists("notes.json"))
        {
            string json = File.ReadAllText("notes.json");
            var notes = JsonConvert.DeserializeObject<List<Note>>(json);
            var note = notes.Find(n => n.Id == id);
            if (note != null)
            {
                note.Status = status;
                string newJson = JsonConvert.SerializeObject(notes);
                File.WriteAllText("notes.json", newJson);
            }
        }
    });
    rootCommand.Add(updateStatusCommand);
    
    var printCommand = new Command("print")
    {
        new Option<bool>(
            "--all",
            getDefaultValue: () => false)
    };
    printCommand.Handler = CommandHandler.Create<bool>((all) =>
    {
        if (File.Exists("notes.json"))
        {
            string json = File.ReadAllText("notes.json");
            var notes = JsonConvert.DeserializeObject<List<Note>>(json);
            if (!all)
            {
                notes = notes.Where(n => n.Status == "Open").ToList();
            }
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
    });
    rootCommand.Add(printCommand);
    
    rootCommand.InvokeAsync(args).Wait();
}
