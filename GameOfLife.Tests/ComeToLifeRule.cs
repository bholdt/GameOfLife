using System.Collections.Generic;
using System.Linq;

namespace GameOfLife.Tests
{
    public class ComeToLifeRule : IGameRule
    {
        private readonly LiveNeighbourCounter _liveNeighbourCounter;

        public ComeToLifeRule(LiveNeighbourCounter liveNeighbourCounter)
        {
            _liveNeighbourCounter = liveNeighbourCounter;
        }

        public ComeToLifeRule() : this(new LiveNeighbourCounter())
        {
            
        }

        public IEnumerable<CellLocation> Apply(IEnumerable<CellLocation> liveCells)
        {
            var liveCellList = liveCells.ToList();
            var eligableDeadCells = new List<CellLocation>();
            foreach(var liveCell in liveCellList)
                eligableDeadCells.AddRange(liveCell.Neighbours());
            return eligableDeadCells.Distinct().Where(e => _liveNeighbourCounter.Count(e, liveCellList) == 3);
        }
    }
}
