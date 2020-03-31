namespace Robots
{
    using System;

    using Robots.Command;
    using Robots.Interfaces;

    public class LrfCommandFactory : ICommandFactory
    {
        private static readonly LeftCommand LeftCommand = new LeftCommand();
        private static readonly RightCommand RightCommand = new RightCommand();
        private static readonly ForwardCommand ForwardCommand = new ForwardCommand();

        public ICommand GetCommand(char c)
        {
            switch (c)
            {
                case 'f':
                case 'F':
                    return ForwardCommand;
                case 'l':
                case 'L':
                    return LeftCommand;
                case 'r':
                case 'R':
                    return RightCommand;
            }

            throw new ArgumentException("Invalid input");
        }

        public bool IsSupported(char c)
        {
            return c == 'f' || c == 'F' || c == 'l' || c == 'L' || c == 'r' || c == 'R';
        }
    }
}