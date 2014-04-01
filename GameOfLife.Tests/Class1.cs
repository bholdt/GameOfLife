using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace GameOfLife.Tests
{
    [TestFixture]
    public class LessThanTwoNeighboursRuleTests
    {
        [Test]
        public void CellsWithLessThanTwoNeighbours_ShouldDie()
        {
            var rule = new LessThanTwoNeighboursRule();

            var liveCells = new[]
                                {
                                    CellCreator.CreateWithNeighbours(0), CellCreator.CreateWithNeighbours(1),
                                    CellCreator.CreateWithNeighbours(1)
                                };

            var evolvedCells = rule.Apply(liveCells);

            Assert.That(evolvedCells.Count(), Is.EqualTo(0));
        }

        [Test]
        public void CellsWithTwoOrMoreNeighbours_ShouldNotDie()
        {
            var rule = new LessThanTwoNeighboursRule();

            var liveCells = new[]
                                {
                                    CellCreator.CreateWithNeighbours(2), CellCreator.CreateWithNeighbours(3),
                                    CellCreator.CreateWithNeighbours(100)
                                };

            var evolvedCells = rule.Apply(liveCells);

            Assert.That(evolvedCells.Count(), Is.EqualTo(3));
        }
    }

    public class CellCreator
    {
        public static ICellLocation CreateWithNeighbours(int neighbours)
        {
            return new FakeCellLocation(neighbours);
        }
    }

    public class FakeCellLocation : ICellLocation
    {
        private readonly int _neighbours;

        public FakeCellLocation(int neighbours)
        {
            _neighbours = neighbours;
        }


        public IEnumerable<ICellLocation> Neighbours()
        {
            for (int i = 0; i < _neighbours; i++)
            {
                yield return new FakeCellLocation(0);
            }
        }
    }

    public interface ICellLocation
    {
        IEnumerable<ICellLocation> Neighbours();
    }

    public class LessThanTwoNeighboursRule
    {
        public IEnumerable<ICellLocation> Apply(IEnumerable<ICellLocation> liveCells)
        {
            return liveCells.Where(l => l.Neighbours().Count() >= 2);
        }
    }
}
