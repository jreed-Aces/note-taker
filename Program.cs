using System.CommandLine;
using System.CommandLine.Invocation;
using System.CommandLine.NamingConventionBinder;
using Newtonsoft.Json;
using System.IO;

// See https://aka.ms/new-console-template for more information
class Program
{
static int Main(string[] args)
{

    var rootCommand = new RootCommand();
    var addCommand = new Command("add", "Add Note");
    addCommand.Add(new Argument<string>
    (
        name: "note",
        description: "Text of note",
        getDefaultValue: () => "No note supplied"
    ));
    addCommand.Handler = CommandHandler.Create<string>((note)=> 
    {
        List<Note> notes;
        int highestId = 0;
        var path = Directory.GetCurrentDirectory();
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

    var printCommand = new Command("print", "Print all notes");
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

    rootCommand.Add(addCommand);
    rootCommand.Add(printCommand);

    return rootCommand.InvokeAsync(args).Result;
}
}