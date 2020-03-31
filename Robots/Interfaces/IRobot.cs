namespace Robots.Interfaces
{
    public interface IRobot
    {
        int X { get; }

        int Y { get; }

        RobotStatus Status { get; set; }

        void MoveForward();

        Point NextPosition();

        Point GetPosition();
    }
}