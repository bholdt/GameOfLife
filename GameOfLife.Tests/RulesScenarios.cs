using System.Collections.Generic;
using NUnit.Framework;
using Should;

namespace GameOfLife.Tests
{
    [TestFixture]
    public class RuleScenarios
    {
        private RuleEngine _engine;

        [SetUp]
        public void GivenGameOfLifeRules()
        {
            _engine = new RuleEngine(new NeighbourCounter());
        }

        [Test]
        public void WhenTickingOneCellWorld_ThenItShouldBeEmpty()
        {
            var world = new World();
            world.AddCell(new Position(0, 0));

            var result = _engine.Tick(world);

            var emptyWorld = new World();
            result.ShouldEqual(emptyWorld);
        }

        [Test]
        public void StilLifeScenario()
        {
            var world = WorldBuilder.Build("##",
                                           "##");

            var result = _engine.Tick(world);

            var stillLifeWorld = WorldBuilder.Build("##", 
                                                    "##");
            result.ShouldEqual(stillLifeWorld);
        }

        [Test]
        public void Blinker()
        {
            var world = WorldBuilder.Build("###");

            var result = _engine.Tick(world);

            var expected = new World();
            expected.AddCell(new Position(-1,-1));
            expected.AddCell(new Position(-1,0));
            expected.AddCell(new Position(-1,1));
            result.ShouldEqual(expected);
        }

    }
}