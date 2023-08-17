using System.CommandLine;
using System.CommandLine.Invocation;
using System.CommandLine.NamingConventionBinder;
using Newtonsoft.Json;
using System.IO;
using note_taker.Services;
using Microsoft.Extensions.DependencyInjection;

// See https://aka.ms/new-console-template for more information
class Program
{
static int Main(string[] args)
{
    var services = new ServiceCollection();
    services.AddSingleton<INoteService, NoteService>();
    var serviceProvider = services.BuildServiceProvider();

    var rootCommand = new RootCommand();
    var addCommand = new Command("add", "Add Note");
    addCommand.Add(new Argument<string>
    (
        name: "note",
        description: "Text of note",
        getDefaultValue: () => "No note supplied"
    ));
    var noteService = serviceProvider.GetService<INoteService>();
    addCommand.Handler = CommandHandler.Create<string>((note)=> 
    {
        noteService.AddNote(note);
    });

    var printCommand = new Command("print", "Print all notes");
    printCommand.Handler = CommandHandler.Create(() =>
    {
        var notes = noteService.PrintNotes();
        foreach (var note in notes)
        {
            Console.WriteLine($"{note.Id}: {note.Timestamp:G}: {note.Text}\n");
        }
    });

    rootCommand.Add(addCommand);
    rootCommand.Add(printCommand);

    return rootCommand.InvokeAsync(args).Result;
}
}