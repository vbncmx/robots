namespace Robots.Interfaces
{
    using System.Threading.Tasks;

    public interface IReporter
    {
        Task ReportAsync(IRobot robot);
    }
}