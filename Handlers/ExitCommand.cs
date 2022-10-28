namespace YSGM.Handlers
{
    public class ExitCommand : BaseCommand
    {
        public string Execute(string[] args)
        {
            Environment.Exit(0);
            return "";
        }
    }
}
