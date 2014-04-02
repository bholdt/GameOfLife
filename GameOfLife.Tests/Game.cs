using System.Collections.Generic;
using System.Linq;

namespace GameOfLife.Tests
{
    public class Game
    {
        private readonly IGameRule[] _rules;

        public Game() : this(new UnderPopulationRule(), new LiveOnRule(), new ComeToLifeRule())
        {
        }

        public Game(params IGameRule[] rules)
        {
            _rules = rules;
        }

        public List<CellLocation> Tick(List<CellLocation> liveCells)
        {
            var newLifeCells = new List<CellLocation>();
            foreach (var rule in _rules)
                newLifeCells.AddRange(rule.Apply(liveCells).ToList());
            return newLifeCells.Distinct().ToList();
        }
    }
}
