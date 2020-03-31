namespace Robots.Command
{
    using System;
    using System.Threading.Tasks;

    using Robots.Interfaces;

    public class ReportingCommandDecorator : ICommand
    {
        private readonly ICommand _baseCommand;

        private readonly IReporter _reporter;

        public ReportingCommandDecorator(ICommand baseCommand, IReporter reporter)
        {
            _baseCommand = baseCommand ?? throw new ArgumentNullException(nameof(baseCommand));
            _reporter = reporter ?? throw new ArgumentNullException(nameof(reporter));
        }

        public async Task ExecuteAsync(IGrid grid, IRobot robot)
        {
            await _baseCommand.ExecuteAsync(grid, robot);

            await _reporter.ReportAsync(robot);
        }
    }
}