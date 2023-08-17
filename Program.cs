using System.CommandLine;
using System.CommandLine.Invocation;
using Newtonsoft.Json;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using note_taker.Services;

// See https://aka.ms/new-console-template for more information
static void Main(string[] args)
{
    var host = Host.CreateDefaultBuilder()
        .ConfigureServices((_, services) =>
            services.AddSingleton<INoteService, NoteService>())
        .Build();

    var noteService = host.Services.GetRequiredService<INoteService>();
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
        var notes = noteService.GetNotes();
        if (notes.Count > 0)
        {
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
