using System;
using Garden.Flowerbed.Model;

namespace Garden.Genetics
{
    public class PlantNode : IEquatable<PlantNode>
    {
        public PlantNode(PlantSegment segment, int height) : this(segment, height, 0)
        {

        }

        public PlantNode(PlantSegment segment, int height, int shift)
        {
            Type = segment;
            Height = height;
            Shift = shift;
        }

        public PlantSegment Type { get; }
        public int Height { get; }
        public int Shift { get; }


        public bool Is(PlantSegment segment)
        {
            return Type == segment;
        }

        public bool Equals(PlantNode other)
        {
            if (ReferenceEquals(null, other))
                return false;
            if (ReferenceEquals(this, other))
                return true;
            return Type == other.Type && Height == other.Height && Shift == other.Shift;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != this.GetType())
                return false;
            return Equals((PlantNode) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (int) Type;
                hashCode = (hashCode * 397) ^ Height;
                hashCode = (hashCode * 397) ^ Shift;
                return hashCode;
            }
        }
    }
}