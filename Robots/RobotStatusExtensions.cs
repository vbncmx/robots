namespace Robots
{
    public static class RobotStatusExtensions
    {
        public static RobotStatus LeftOf(this RobotStatus status)
        {
            return status switch
            {
                RobotStatus.N => RobotStatus.W,
                RobotStatus.W => RobotStatus.S,
                RobotStatus.S => RobotStatus.E,
                RobotStatus.E => RobotStatus.N,
                _ => RobotStatus.LOST
            };
        }

        public static RobotStatus RightOf(this RobotStatus status)
        {
            return status switch
            {
                RobotStatus.N => RobotStatus.E,
                RobotStatus.E => RobotStatus.S,
                RobotStatus.S => RobotStatus.W,
                RobotStatus.W => RobotStatus.N,
                _ => RobotStatus.LOST
            };
        }
    }
}