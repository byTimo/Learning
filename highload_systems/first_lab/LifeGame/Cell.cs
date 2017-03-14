namespace LifeGame
{
    public struct Cell
    {
        public int X { get; }
        public int Y { get; }

        private Cell(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static Cell Create(int x, int y)
        {
            return new Cell(x, y);
        }

        public bool Equals(Cell other)
        {
            return X == other.X && Y == other.Y;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Cell && Equals((Cell)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (X * 397) ^ Y;
            }
        }

        public override string ToString()
        {
            return $"{X},{Y}";
        }
    }
}