namespace Robots.Command
{
    using System;
    using System.Threading.Tasks;

    using Robots.Interfaces;

    public class ForwardCommand : ICommand
    {
        public Task ExecuteAsync(IGrid grid, IRobot robot)
        {
            if (grid == null)
                throw new ArgumentNullException(nameof(grid));
            if (robot == null)
                throw new ArgumentNullException(nameof(robot));

            var initialPosition = robot.GetPosition();

            if (grid.IsScent(initialPosition))
            {
                ScentForward(robot, grid);
            }
            else
            {
                Forward(robot, grid, initialPosition);
            }

            return Task.FromResult(0);
        }

        private void ScentForward(IRobot robot, IGrid grid)
        {
            var nextPosition = robot.NextPosition();
            if (!grid.IsOutside(nextPosition))
            {
                robot.MoveForward();
            }
        }

        private void Forward(IRobot robot, IGrid grid, Point initialPosition)
        {
            robot.MoveForward();
            var position = robot.GetPosition();
            if (grid.IsOutside(position))
            {
                robot.Status = RobotStatus.LOST;
                grid.AddScent(initialPosition);
            }
        }
    }
}