using YSGM;

Console.WriteLine(@"
██╗   ██╗███████╗ ██████╗ ███╗   ███╗
╚██╗ ██╔╝██╔════╝██╔════╝ ████╗ ████║
 ╚████╔╝ ███████╗██║  ███╗██╔████╔██║
  ╚██╔╝  ╚════██║██║   ██║██║╚██╔╝██║
   ██║   ███████║╚██████╔╝██║ ╚═╝ ██║
   ╚═╝   ╚══════╝ ╚═════╝ ╚═╝     ╚═╝
                                     
");
CommandMap.RegisterAll();

// Create prompt interface

while (true)
{
    Console.Write("> ");
    var userInput = Console.ReadLine();
    string[] split = userInput!.Split(' ');
    string cmd = split[0];
    // If the user entered a valid command, execute it.
    if (CommandMap.handlers.ContainsKey(cmd))
    {
        var handler = CommandMap.handlers[cmd];
        var arguments = split.Skip(1).ToArray();
        Console.WriteLine(handler.Execute(arguments));
    }
    else
    {
        Console.WriteLine("Invalid command.");
    }
}