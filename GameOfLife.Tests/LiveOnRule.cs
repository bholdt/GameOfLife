using System.Collections.Generic;
using System.Linq;

namespace GameOfLife.Tests
{
    public class LiveOnRule : IGameRule
    {
        private readonly LiveNeighbourCounter _liveNeighbourCounter;

        public LiveOnRule() : this(new LiveNeighbourCounter())
        {
            
        }

        public LiveOnRule(LiveNeighbourCounter liveNeighbourCounter)
        {
            _liveNeighbourCounter = liveNeighbourCounter;
        }

        public IEnumerable<CellLocation> Apply(IEnumerable<CellLocation> liveCells)
        {
            var liveCellList = liveCells.ToList();
            return liveCellList.Where( l => _liveNeighbourCounter.Count(l, liveCellList) == 2 || _liveNeighbourCounter.Count(l, liveCellList) == 3);
        }
    }
}
