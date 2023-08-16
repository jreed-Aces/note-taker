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
        Console.WriteLine("Your note was: " + note);
        int lastId = int.Parse(File.ReadAllText("id.txt"));
        Note newNote = new Note(lastId + 1, note);
        File.WriteAllText("id.txt", (lastId + 1).ToString());
        List<Note> notes;
        if (File.Exists("notes.json"))
        {
            string json = File.ReadAllText("notes.json");
            notes = JsonConvert.DeserializeObject<List<Note>>(json);
            foreach (var note in notes)
            {
                note.Id = int.Parse(note.Id.ToString());
            }
        }
        else
        {
            notes = new List<Note>();
        }
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
                Console.WriteLine($"{note.Timestamp:G}: {note.Text}\n");
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
