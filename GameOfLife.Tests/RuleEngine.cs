using System;
using System.Collections.Generic;

namespace GameOfLife.Tests
{
    public class RuleEngine
    {
        private readonly NeighbourCounter _counter;
        private Dictionary<CellInfo, IApplyRule> _rules;

        public RuleEngine(NeighbourCounter counter)
        {
            _counter = counter;
            setupRules();
        }

        public World Tick(World world)
        {
            var eligibleCells = _counter.CreateEligibleCells(world);
            var newWorld = new World();
            foreach(var neighbour in eligibleCells)
                applyRule(neighbour, newWorld);
            
            return newWorld;
        }

        private void applyRule(KeyValuePair<Position, CellInfo> neighbour, World newWorld)
        {
            try
            {
                _rules[neighbour.Value].ApplyRule(newWorld, neighbour.Key);
            }
            catch (Exception)
            {
            }
        }

        private void setupRules()
        {
            _rules = new Dictionary<CellInfo, IApplyRule>();
            _rules.Add(new CellInfo() {CellState = CellState.Alive, NumberOfNeighbours = 2}, new StayAliveRule());
            _rules.Add(new CellInfo() {CellState = CellState.Alive, NumberOfNeighbours = 3}, new StayAliveRule());
            _rules.Add(new CellInfo() {CellState = CellState.Dead, NumberOfNeighbours = 3}, new StayAliveRule());
        }
    }
//                rules[neighbour.CellInfo].ApplyRule
//                if(neighbour.Value.NumberOfNeighbours == 2 && neighbour.Value.CellState == CellState.Alive)
//                    newWorld.AddCell(neighbour.Key);
//                if(neighbour.Value.NumberOfNeighbours == 3)
//                    newWorld.AddCell(neighbour.Key);

    public class StayAliveRule : IApplyRule
    {
        public void ApplyRule(World newWorld, Position cell)
        {
            newWorld.AddCell(cell);
        }
    }


    public interface IApplyRule
    {
        void ApplyRule(World newWorld, Position cell);
    }

    public class CellInfo
    {
        public bool Equals(CellInfo other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.CellState == CellState && other.NumberOfNeighbours == NumberOfNeighbours;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (CellInfo)) return false;
            return Equals((CellInfo) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (CellState.GetHashCode()*397) ^ NumberOfNeighbours;
            }
        }

        public CellState CellState { get; set; }
        public int NumberOfNeighbours { get; set; }
    }

    public enum CellState
    {
        Alive,
        Dead
    }

    public class NeighbourCounter
    {
        private Dictionary<Position, CellInfo> _numberOfNeighbours = new Dictionary<Position, CellInfo>();

        public Dictionary<Position, CellInfo> CreateEligibleCells(World world)
        {
            foreach (var liveCell in world.Cells)
            {
                makeCellAlive(liveCell);
                addNeighbours(liveCell);
            }
            return _numberOfNeighbours;
        }

        private void addNeighbours(Position liveCell)
        {
            addNeighbour(new Position(liveCell.X + 1, liveCell.Y + 1));
            addNeighbour(new Position(liveCell.X + 1, liveCell.Y - 1));
            addNeighbour(new Position(liveCell.X + 1, liveCell.Y));
            addNeighbour(new Position(liveCell.X - 1, liveCell.Y + 1));
            addNeighbour(new Position(liveCell.X - 1, liveCell.Y - 1));
            addNeighbour(new Position(liveCell.X - 1, liveCell.Y));
            addNeighbour(new Position(liveCell.X, liveCell.Y + 1));
            addNeighbour(new Position(liveCell.X, liveCell.Y - 1));
        }

        private void addNeighbour(Position position)
        {
            addCellToTrack(position);
            _numberOfNeighbours[position].NumberOfNeighbours++;
        }

        private void makeCellAlive(Position liveCell)
        {
            if (!_numberOfNeighbours.ContainsKey(liveCell))
                _numberOfNeighbours.Add(liveCell, new CellInfo() {CellState = CellState.Alive});
            else
                _numberOfNeighbours[liveCell].CellState = CellState.Alive;
        }

        private void addCellToTrack(Position position)
        {
            if (!_numberOfNeighbours.ContainsKey(position))
                _numberOfNeighbours.Add(position, new CellInfo() {CellState = CellState.Dead});
        }
    }


}
