using System.CommandLine;
using System.CommandLine.Invocation;
using Newtonsoft.Json;
using System.IO;
using note_taker.Services;

// See https://aka.ms/new-console-template for more information
static void Main(string[] args, INoteService noteService)
{
    var rootCommand = new RootCommand
    {
        new Option<string>(
            "--note",
            getDefaultValue: () => "No note provided")
    };

    rootCommand.Handler = CommandHandler.Create<string>((note) =>
    {
        noteService.AddNote(note);
    });

    var printCommand = new Command("print");
    printCommand.Handler = CommandHandler.Create(() =>
    {
        noteService.PrintAllNotes();
    });
    rootCommand.Add(printCommand);
    rootCommand.InvokeAsync(args).Wait();
}
