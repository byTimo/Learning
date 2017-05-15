using System;

namespace Garden.Flowerbed.Model
{
    public struct GrowingPosition : IEquatable<GrowingPosition>
    {
        public int Height { get; }
        public int Shift { get;  }

        public GrowingPosition(int height, int shift)
        {
            Height = height;
            Shift = shift;
        }

        public static GrowingPosition Create(int height, int shift)
        {
            return new GrowingPosition(height, shift);
        }

        public bool Equals(GrowingPosition other)
        {
            return Height == other.Height && Shift == other.Shift;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            return obj is GrowingPosition && Equals((GrowingPosition) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Height*397) ^ Shift;
            }
        }
    }
}