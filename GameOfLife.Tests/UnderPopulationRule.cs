using System.Collections.Generic;
using System.Linq;

namespace GameOfLife.Tests
{
    public class UnderPopulationRule : IGameRule
    {
        private readonly LiveNeighbourCounter _liveNeighbourCounter;

        public UnderPopulationRule(LiveNeighbourCounter liveNeighbourCounter)
        {
            _liveNeighbourCounter = liveNeighbourCounter;
        }

        public UnderPopulationRule() : this(new LiveNeighbourCounter())
        {
            
        }

        public IEnumerable<CellLocation> Apply(IEnumerable<CellLocation> liveCells)
        {
            var liveCellList = liveCells.ToList();
            return liveCellList.Where(l => _liveNeighbourCounter.Count(l, liveCellList) >= 2);
        }
    }
}