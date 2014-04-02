using System.Collections.Generic;
using System.Linq;
using System.Threading;
using GameOfLife.Tests;

namespace GameOfLife.Console
{
    class Program
    {
        public static void Main(string[] args)
        {
            var game = new Game();
            var liveCells = new List<CellLocation>()
                                        {
                                            new CellLocation(10, 2), new CellLocation(10, 4), new CellLocation(10, 3),
                                            new CellLocation(20,6), new CellLocation(20,7), new CellLocation(21,6), new CellLocation(21,7), new CellLocation(11,7)
                                        };
            Display(liveCells);
            Thread.Sleep(1000);
            for (int i = 0; i < 100; i++)
            {
                liveCells = game.Tick(liveCells).ToList();
                Thread.Sleep(1000);
                Display(liveCells);
            }

        }

        private static void Display(IEnumerable<CellLocation> liveCells)
        {
            System.Console.Clear();
            foreach (var liveCell in liveCells)
            {
                System.Console.SetCursorPosition(liveCell.X, liveCell.Y);
                System.Console.Write("#");
            }
        }
    }

}
