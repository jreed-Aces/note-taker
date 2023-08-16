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

    rootCommand.InvokeAsync(args).Wait();
}
