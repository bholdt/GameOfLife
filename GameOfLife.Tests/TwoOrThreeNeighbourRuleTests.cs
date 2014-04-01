using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace GameOfLife.Tests
{
    [TestFixture]
    public class DeadCellWithThreeNeighbourRuleTests
    {
        [Test]
        public void DeadCellsWithThreeOrMoreNeighbours_ShouldComeAlive()
        {
            var rule = new DeadCellWithMoreThanThreeNeighbourRule();

            var liveCells = new[]
                                {
                                    CellCreator.CreateWithNeighbours(new FakeCellLocation(3), new FakeCellLocation(3), new FakeCellLocation(4))
                                };

            var evolvedCells = rule.Apply(liveCells);

            Assert.That(evolvedCells.Count(), Is.EqualTo(3));
 
        }

    }

    public class DeadCellWithMoreThanThreeNeighbourRule : IGameRule
    {
        public IEnumerable<ICellLocation> Apply(IEnumerable<ICellLocation> liveCells)
        {
            var eligibleDeadCells = new List<ICellLocation>();
            var liveCellList = liveCells.ToList();
            foreach(var liveCell in liveCellList)
                eligibleDeadCells.AddRange(liveCell.Neighbours().Where(n => !liveCellList.Contains(n)));
            return eligibleDeadCells.Where(e => e.Neighbours().Count() > 2);
        }
    }
}
