using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace GameOfLife.Tests
{
    [TestFixture]
    public class MoreThanThreeNeighbourRuleTests
    {
        [Test]
        public void CellsWithMoreThanThreeNeighbours_ShouldDie()
        {
            var rule = new MoreThanThreeNeighboursRule();

            var liveCells = new[]
                                {
                                    CellCreator.CreateWithNeighbours(4), CellCreator.CreateWithNeighbours(5),
                                    CellCreator.CreateWithNeighbours(6)
                                };

            var evolvedCells = rule.Apply(liveCells);

            Assert.That(evolvedCells, Is.EqualTo(new ICellLocation[0]));
        }

        [Test]
        public void CellsWithLessThanThreeNeighbours_ShouldStayAlive()
        {
            var rule = new MoreThanThreeNeighboursRule();

            var liveCells = new[]
                                {
                                    CellCreator.CreateWithNeighbours(2), CellCreator.CreateWithNeighbours(1),
                                    CellCreator.CreateWithNeighbours(1)
                                };

            var evolvedCells = rule.Apply(liveCells);

            Assert.That(evolvedCells, Is.EqualTo(liveCells));
        }
    }

    public class MoreThanThreeNeighboursRule : IGameRule
    {
        public IEnumerable<ICellLocation> Apply(IEnumerable<ICellLocation> liveCells)
        {
            return liveCells.Where(l => l.Neighbours().Count() < 3);
        }
    }

    public interface IGameRule
    {
        IEnumerable<ICellLocation> Apply(IEnumerable<ICellLocation> liveCells);
    }
}
