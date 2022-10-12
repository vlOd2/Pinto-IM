namespace PintoChatNS.PintoConsole
{
    public interface IConsoleCommand : IConsoleCallable
    {
        string GetUsage();
        int GetMinArgsCount();
        int GetMaxArgsCount();
        void Execute(string[] args);
    }
}
