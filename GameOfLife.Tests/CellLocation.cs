using System.Collections.Generic;

namespace GameOfLife.Tests
{
    public class CellLocation 
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        public CellLocation(int x, int y)
        {
            X = x;
            Y = y;
        }

        public IEnumerable<CellLocation> Neighbours()
        {
            return new List<CellLocation>(){
                                            new CellLocation(X-1,Y-1),
                                            new CellLocation(X-1,Y),
                                            new CellLocation(X-1,Y + 1),
                                            new CellLocation(X+1,Y-1),
                                            new CellLocation(X+1,Y),
                                            new CellLocation(X+1,Y+1),
                                            new CellLocation(X,Y-1),
                                            new CellLocation(X,Y+1),
                                        };
        }

        protected bool Equals(CellLocation other)
        {
            return X == other.X && Y == other.Y;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((CellLocation) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (X*397) ^ Y;
            }
        }
    }
}