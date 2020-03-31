Robots - core project
Robots.Console - console client for Robots
Robots.Tests - test cover for Robots

Main interfaces are IGrid - represents the grid, IRobot - represents the robot, ICommand - represents the command

There is only one implementation for IGrid interface - very simple: width, height + collection to store scents

There is only one implementation for IRobots interface - very simple: x, y coordinates + status (N,W,E,S,LOST)

ICommand has several implementations:

1. LeftCommand - rotates robot left
2. RightCommand - rotates robot right
3. ForwardCommand - moves robot, checks if robot is outside bounds, adds sents to the grid
4. TextCommand - constructs and executes subcommands based on a string input (FLFRFF...).
For construction of subcommands the ICommandFactory interface is used.

My first intention was to construct a single implementation for ICommand interface with a switch inside,
but then i realized that the class has too many responsibilities and decomposed it into Left, Right, Forward and Text commands.
This allows to make classes simpler and allows to unit test better.