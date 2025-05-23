namespace Desmux.System.CRUSH.Core.SystemCommands;

public class HelpCommand : ICommand
{
    public string Name => "help";
    public string Desc => "List all registered commands";
    public void Execute(string[] args) => CommandManager.ListCommands();
} 