namespace YSGM.Handlers
{
    public class MUIPCommand : BaseCommand
    {
        public string Execute(string[] args)
        {
            if (args.Length < 2)
            {
                return "Usage: muip <uid> <command> [msg]";
            }
            string uid = args[0];
            int cmd = Convert.ToInt32(args[1]);
            string msg = string.Join(" ", args.Skip(2));
            return MUIPManager.Instance.GET(uid, cmd, msg);
        }
    }
}
