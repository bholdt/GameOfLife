using System.Collections.Generic;

namespace GameOfLife.Tests
{
    public interface IGameRule
    {
        IEnumerable<CellLocation> Apply(IEnumerable<CellLocation> liveCells);
    }
}