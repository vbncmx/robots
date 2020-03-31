namespace Robots.Console
{
    using System;
    using System.Threading.Tasks;

    using Robots.Interfaces;

    public class ConsoleReporter : IReporter
    {
        public Task ReportAsync(IRobot robot)
        {
            Console.WriteLine($"{robot.X} {robot.Y} {robot.Status}");

            return Task.FromResult(0);
        }
    }
}