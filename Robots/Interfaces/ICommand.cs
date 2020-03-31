namespace Robots.Interfaces
{
    using System.Threading.Tasks;

    public interface ICommand
    {
        Task ExecuteAsync(IGrid grid, IRobot robot);
    }
}