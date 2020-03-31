namespace Robots
{
    using System;

    using Robots.Interfaces;

    public class Robot : IRobot
    {
        private int _x;
        private int _y;
        public int X => _x;
        public int Y => _y;
        public RobotStatus Status { get; set; }

        private Robot() { }

        public static Robot Construct(string input, IGrid grid)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            var split = input.Split(' ');

            if (split.Length != 3 ||
                !int.TryParse(split[0], out var x) ||
                !int.TryParse(split[1], out var y) ||
                !Enum.TryParse(split[2], out RobotStatus status))
                throw new ArgumentException("Invalid robot input", nameof(input));

            if (x < 0 || x > grid.Width || y < 0 || y > grid.Height)
                throw new ArgumentException("Invalid robot position");

            var robot = new Robot { _x = x, _y = y, Status = status };

            return robot;
        }

        public void MoveForward()
        {
            var point = NextPosition();

            _x = point.X;
            _y = point.Y;
        }

        public Point NextPosition()
        {
            switch (Status)
            {
                case RobotStatus.E:
                    return new Point(_x + 1, _y);
                case RobotStatus.W:
                    return new Point(_x - 1, _y);
                case RobotStatus.S:
                    return new Point(_x, _y - 1);
                case RobotStatus.N:
                    return new Point(_x, _y + 1);
            }

            return new Point(_x, _y);
        }

        public Point GetPosition()
        {
            return new Point(_x, _y);
        }
    }
}
