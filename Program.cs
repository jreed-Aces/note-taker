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
        Note newNote = new Note(note);
        string json = JsonConvert.SerializeObject(newNote);
        File.WriteAllText($"{newNote.Id}.json", json);
    });

    var printCommand = new Command("print");
    printCommand.Handler = CommandHandler.Create(() =>
    {
        var files = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.json");
        var notes = new List<Note>();
        foreach (var file in files)
        {
            var json = File.ReadAllText(file);
            var note = JsonConvert.DeserializeObject<Note>(json);
            notes.Add(note);
        }
        notes.Sort((x, y) => DateTime.Compare(x.Timestamp, y.Timestamp));
        foreach (var note in notes)
        {
            Console.WriteLine($"{note.Timestamp:G}: {note.Text}\n");
        }
    });
    rootCommand.Add(printCommand);
    rootCommand.InvokeAsync(args).Wait();
}
