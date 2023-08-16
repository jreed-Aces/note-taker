using System.CommandLine;
using System.CommandLine.Invocation;

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
    });

    rootCommand.InvokeAsync(args).Wait();
}
