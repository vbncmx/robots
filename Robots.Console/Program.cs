using System;

namespace Robots.Console
{
    using Robots.Command;
    using Robots.Interfaces;

    using Console = System.Console;

    class Program
    {
        private static readonly ICommandFactory CommandFactory = new LrfCommandFactory();
        private static readonly IReporter Reporter = new ConsoleReporter();

        static void Main(string[] args)
        {
            var grid = InitGrid();

            while (true)
            {
                var robot = InitRobot(grid);

                InstructionLoop(grid, robot);

                Console.WriteLine("One more robot (y/n)?");
                var input = Console.ReadLine();
                if (input != "y")
                {
                    return;
                }
            }
        }

        private static void InstructionLoop(Grid grid, Robot robot)
        {
            while (true)
            {
                Console.WriteLine("Enter instructions:");

                var instructions = Console.ReadLine();
                
                try
                {
                    var command = ConstructCommand(instructions);

                    command.ExecuteAsync(grid, robot).Wait();

                    if (robot.Status == RobotStatus.LOST)
                    {
                        return;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        private static ICommand ConstructCommand(string instructions)
        {
            ICommand command = new TextCommand(instructions, CommandFactory);

            command = new ReportingCommandDecorator(command, Reporter);

            return command;
        }

        private static Grid InitGrid()
        {
            while (true)
            {
                Console.WriteLine("Enter grid data:");

                var gridInput = Console.ReadLine();

                try
                {
                    return Grid.Construct(gridInput);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        private static Robot InitRobot(Grid grid)
        {
            while (true)
            {
                Console.WriteLine("Enter robot data:");

                var robotInput = Console.ReadLine();

                try
                {
                    return Robot.Construct(robotInput, grid);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}
