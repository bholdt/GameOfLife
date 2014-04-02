using System.Collections.Generic;
using System.Linq;

namespace GameOfLife.Tests
{
    public class LiveNeighbourCounter
    {
        public int Count(CellLocation position, IEnumerable<CellLocation> liveCells)
        {
            return position.Neighbours().Count(liveCells.Contains);
        }
    }
}