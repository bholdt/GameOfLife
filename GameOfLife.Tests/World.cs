using System.Collections;
using System.Collections.Generic;

namespace GameOfLife.Tests
{
    public class World
    {
        HashSet<Position> _cells = new HashSet<Position>();

        public IEnumerable<Position> Cells { get { return _cells; } }

        public void AddCell(Position position)
        {
            _cells.Add(position);
        }

        public override int GetHashCode()
        {
            return (_cells != null ? _cells.GetHashCode() : 0);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (World)) return false;
            return Equals((World) obj);
        }

        public bool Equals(World other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return _cells.SetEquals(other._cells);
        }
    }
}