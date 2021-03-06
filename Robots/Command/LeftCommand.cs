﻿namespace Robots.Command
{
    using System;
    using System.Threading.Tasks;

    using Robots.Interfaces;

    public class LeftCommand : ICommand
    {
        public Task ExecuteAsync(IGrid grid, IRobot robot)
        {
            if (robot == null)
                throw new ArgumentNullException(nameof(robot));

            robot.Status = robot.Status.LeftOf();

            return Task.FromResult(0);
        }
    }
}