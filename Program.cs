// See https://aka.ms/new-console-template for more information
static void Main(string[] args)
{
    string note = args.Length > 0 ? args[0] : "No note provided";
    Console.WriteLine("Your note was: " + note);
}
