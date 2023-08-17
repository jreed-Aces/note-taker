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

    using System.CommandLine;
    using System.CommandLine.Invocation;
    using System.CommandLine.NamingConventionBinder;
    using Newtonsoft.Json;
    using System.IO;
    using note_taker.Services;
    using Microsoft.Extensions.DependencyInjection;
    
    class Program
    {
        static int Main(string[] args)
        {
            // existing code...
    
            var statusCommand = new Command("status", "Update note status");
            statusCommand.Add(new Argument<int>("id", "Note id"));
            statusCommand.Add(new Argument<string>("status", "New status"));
            statusCommand.Handler = CommandHandler.Create<int, string>((id, status) =>
            {
                noteService.UpdateStatus(id, Enum.Parse<Status>(status, true));
            });
    
            var addCommand = new Command("add", "Add Note");
            addCommand.Add(new Argument<string>
            (
                name: "note",
                description: "Text of note",
                getDefaultValue: () => "No note supplied"
            ));
            addCommand.Handler = CommandHandler.Create<string>((note)=> 
            {
                noteService.AddNote(note);
            });

            var printCommand = new Command("print", "Print all notes");
            printCommand.Add(new Option<bool>("--all", "Print all notes"));
            printCommand.Handler = CommandHandler.Create<bool>((all) =>
            {
                var notes = noteService.PrintNotes(all);
                foreach (var note in notes)
                {
                    Console.WriteLine($"{note.Id}: {note.Timestamp:G}: {note.Text} ({note.NoteStatus})\n");
                }
            });

            rootCommand.Add(addCommand);
            rootCommand.Add(printCommand);
            rootCommand.Add(statusCommand);

            return rootCommand.InvokeAsync(args).Result;
        }
    }
}