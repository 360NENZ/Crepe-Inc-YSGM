using YSGM.Handlers;

namespace YSGM
{
    public class CommandMap
    {
        public static Dictionary<string, BaseCommand> handlers = new();

        public static void RegisterAll()
        {
            handlers.Add("shell", new ShellCommand()); // SSH shell
            
            // Exit app
            handlers.Add("exit", new ExitCommand());
            handlers.Add("quit", new ExitCommand());
        }
    }
}
