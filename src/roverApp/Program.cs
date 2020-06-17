using System;
using System.Collections.Generic;
using common.interfaces;
using common.models;
using Microsoft.Extensions.DependencyInjection;
using StateMachine;

namespace roverApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceprovider = new ServiceCollection()
                .AddSingleton<IDirectionState, DirectionManager>()
                .AddSingleton<IStateMachine, StateMachine.StateMachine>()
                .BuildServiceProvider();

            var roverManager = serviceprovider.GetService<IStateMachine>();
            Console.WriteLine("Let's move some rovers around by press <ENTER> key to start");

            while (Console.ReadKey().Key!= ConsoleKey.Escape)
            {
                Console.WriteLine("\nPlease enter the upper-right coordinates separated by space : x y");
                string boundaryLine = Console.ReadLine();
                int xpos, ypos;
                var firstline = boundaryLine.Split(' ');
                try
                {
                    xpos = int.Parse(firstline[0]);
                    ypos = int.Parse(firstline[1]);
                    roverManager.SetGridBoundary(new GridPosition { XPos = xpos, YPos = ypos });
                }
                catch (Exception ex)
                {
                    Console.BackgroundColor = ConsoleColor.Blue;
                    Console.WriteLine(ex.Message);
                    Console.BackgroundColor = ConsoleColor.Black;
                }

                bool allRoversAreDone = false;
                List<string> outputs = new List<string>();
                Console.WriteLine("For each rover, input x and y coordinate," +
                      " and facing direction (one of N S E W), then press ENTER, ");

                Console.WriteLine("input command sequence such as LMLMR. When you finish entering commands for all Rovers, Press ENTER TWICE to see your results.");
                while (!allRoversAreDone)
                {
                    //reading rover position and commands
                    string roverState = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(roverState)) break;

                    string commandSeq = Console.ReadLine();
                    Direction direction;
                    try
                    {
                        var roverLine = roverState.Split(' ');
                        xpos = int.Parse(roverLine[0]);
                        ypos = int.Parse(roverLine[1]);
                        direction = (Direction)roverLine[2].ToUpperInvariant()[0];

                    }
                    catch (InvalidCastException ex)
                    {
                        Console.BackgroundColor = ConsoleColor.Blue;
                        Console.WriteLine(ex.Message);
                        Console.BackgroundColor = ConsoleColor.Black;
                        return;
                    }
                    var initState = new State
                    {
                        FaceDirection = direction,
                        Position = new GridPosition { XPos = xpos, YPos = ypos }
                    };
                    var result = roverManager.GetState(initState, commandSeq);
                    outputs.Add(result.ToString());
                }

                Console.WriteLine("---------------------- Your Results: ---------------------");
                outputs.ForEach(x => Console.WriteLine(x));

                roverManager.Reset();

                Console.WriteLine("Press esc if you want to exit, otherwise, press Enter to play another round!");
            }
        }
    }
}
