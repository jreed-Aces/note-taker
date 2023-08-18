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
    services.AddSingleton<IFileService, FileService>();
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
    printCommand.Add(new Option<bool>("--all", getDefaultValue: () => false));
    printCommand.Handler = CommandHandler.Create<bool>((all) =>
    {
        var notes = noteService.PrintNotes(all);
        foreach (var note in notes)
        {
            Console.WriteLine($"{note.Id}: {note.Timestamp:G}: {note.Text}\n");
        }
    });

    rootCommand.Add(addCommand);
    var updateCommand = new Command("update", "Update note status");
    updateCommand.Add(new Argument<int>("id"));
    updateCommand.Add(new Argument<Note.Status>("status"));
    updateCommand.Handler = CommandHandler.Create<int, Note.Status>((id, status) =>
    {
        noteService.UpdateNoteStatus(id, status);
    });

    rootCommand.Add(printCommand);
    rootCommand.Add(updateCommand);

    return rootCommand.InvokeAsync(args).Result;
}
}