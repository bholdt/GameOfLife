using System.Linq;
using NUnit.Framework;

namespace GameOfLife.Tests
{
    [TestFixture]
    public class UnderPopulationRuleTests
    {
        [Test]
        public void CellsWithLessThanTwoNeighbours_ShouldDie()
        {
            var rule = new UnderPopulationRule();

            var liveCells = new[] { new CellLocation(0,0), new CellLocation(1,0) };

            var evolvedCells = rule.Apply(liveCells);

            Assert.That(evolvedCells.Count(), Is.EqualTo(0));
        }

        [Test]
        public void CellsWithTwoOrMoreNeighbours_ShouldStayAlive()
        {
            var rule = new UnderPopulationRule();

            var liveCells = new[] { new CellLocation(0,0), new CellLocation(1,0), new CellLocation(0,1) };

            var evolvedCells = rule.Apply(liveCells);

            Assert.IsTrue(evolvedCells.Contains(new CellLocation(1, 0)));
        }
    }
}
