using System.ComponentModel;
using NUnit.Framework;

namespace GameOfLife.Tests
{
    [TestFixture]
    public class LiveOnRuleTests
    {
        [Test]
        public void CellsWithExactlyTwoLiveNeighbours_ShouldStayAlive()
        {
            var liveOnRule = new LiveOnRule();

            var evolvedCells = liveOnRule.Apply(new[] {new CellLocation(0, 0), new CellLocation(0, 1), new CellLocation(0, 2)});

            Assert.That(evolvedCells, Is.EquivalentTo(new[]{new CellLocation(0,1)}));
        }
    }
}
