namespace Robots.Interfaces
{
    public interface ICommandFactory
    {
        ICommand GetCommand(char c);
        bool IsSupported(char c);
    }
}