# Rovers On Mars

This is a .NetCore 3.1 console app that solves the Mars Rovers problem. The solution includes four projects: the consoleApp, the stateMachine, common which defines interfaces and models, and tests.

INPUT:
The first line of input is the upper-right coordinates of the plateau, the
lower-left coordinates are assumed to be 0,0.

The rest of the input is information pertaining to the rovers that have
been deployed. Each rover has two lines of input. The first line gives the
rover's position (X Y N) where X and Y are integers and N is one of the letters in (N W E S) indicating the rover's orientation.
The second line is a sequence of commands such as LMRL where L/R means turning L/R,
M means move forward a grid depending on the rover's orientation.

The console app input is not quite user friendly. The input validation is primitive, but that was not my focus in this exercise. In addition, you have to press enter twice to indicate all rover inputs are done. Sorry :(

There are many enhancement can be done. 1. logging. 2. error checking/handling. For instance, the user input has to follow the exact guidelines, otherwise, it will just throw excption. Another example is that we should check if the initial state/position 
is off the grid. 3. Currently if a command causes a rover to be off the grid, we do not throw exception nor log, instead we do not make a move,
and continue with the next command. This is a reasonable because it behaves the same way when a move causes the rover to collide with another rover
who stays on the grid once it was deployed.

Other solutions could achieve the same result, maybe related to a graph? But I am not familiar with graph theory, so I took the applied approach.
