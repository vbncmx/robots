namespace Robots.Command
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Robots.Interfaces;

    public class TextCommand : ICommand
    {
        private readonly ICommandFactory _commandFactory;


        private readonly string _instructions;

        public TextCommand(string instructions, ICommandFactory commandFactory)
        {
            _commandFactory = commandFactory ?? throw new ArgumentNullException(nameof(commandFactory));
            if (instructions == null)
                throw new ArgumentNullException(nameof(instructions));
            if (instructions.Any(c => !_commandFactory.IsSupported(c)))
                throw new ArgumentException("Invalid instructions");
            _instructions = instructions;
        }

        public async Task ExecuteAsync(IGrid grid, IRobot robot)
        {
            foreach (var instruction in _instructions)
            {
                var command = _commandFactory.GetCommand(instruction);

                await command.ExecuteAsync(grid, robot);

                if (robot.Status == RobotStatus.LOST)
                {
                    break;
                }
            }
        }
    }
}