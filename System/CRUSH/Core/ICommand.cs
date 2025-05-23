namespace Desmux.System.CRUSH.Core
{
    public interface ICommand
    {
        string Name { get; }
        string Desc { get; }

        void Execute(string[] args);
    }
}
