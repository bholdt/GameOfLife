using System.Linq;
using NUnit.Framework;

namespace GameOfLife.Tests
{
    [TestFixture]
    public class ComeToLifeRuleTests
    {
        [Test]
        public void AnyDeadCellWithMoreThanThreeLiveNeighbours_ShouldComeAlive()
        {
            var comeToLifeRule = new ComeToLifeRule();
            var liveCells = new[] {new CellLocation(0, 0), new CellLocation(0, 1), new CellLocation(0, 2)};

            var evolvedCells = comeToLifeRule.Apply(liveCells);

            Assert.True(evolvedCells.Contains(new CellLocation(1,1)));
            Assert.True(evolvedCells.Contains(new CellLocation(-1,1)));
        }
    }
}
