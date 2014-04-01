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
        public void CellsWithTwoOrMoreNeighbours_ShouldStayAlive()
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

        public static ICellLocation CreateWithNeighbours(params ICellLocation[] locations)
        {
            return new FakeCellLocation(locations);
        }
    }

    public class FakeCellLocation : ICellLocation
    {
        private IList<ICellLocation> _neighbours = new List<ICellLocation>(); 

        public FakeCellLocation(int neighbours)
        {
            for (int i = 0; i < neighbours; i++)
            {
                _neighbours.Add(new FakeCellLocation(new ICellLocation[0]));
            }
        }
        public FakeCellLocation(IEnumerable<ICellLocation> locations)
        {
            _neighbours = locations.ToList();
        }

        public IEnumerable<ICellLocation> Neighbours()
        {
            return _neighbours;
        }
    }

    public interface ICellLocation
    {
        IEnumerable<ICellLocation> Neighbours();
    }

    public class LessThanTwoNeighboursRule : IGameRule
    {
        public IEnumerable<ICellLocation> Apply(IEnumerable<ICellLocation> liveCells)
        {
            return liveCells.Where(l => l.Neighbours().Count() >= 2);
        }
    }
}
