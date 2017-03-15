using System;
using System.Collections.Generic;
using System.Linq;

namespace Garden.Flowerbed
{
    internal class Plant
    {
        private static readonly IEnumerable<PlantPart> startParts = new[] {PlantPart.Seed};
        private static readonly Random random = new Random();

        private readonly Stack<PlantPart> parts;

        public Plant() : this(Enumerable.Empty<PlantPart>()) { }

        public Plant(IEnumerable<PlantPart> plantParts)
        {
            parts = new Stack<PlantPart>(startParts.Concat(plantParts));
        }

        public PlantPart[] Parts => parts.Reverse().ToArray();

        public Plant Grow()
        {
            parts.Push(PlantPart.Sqrout);
            return this;
        }

        public Plant Grow(double flowerProbability)
        {
            if (parts.Peek() == PlantPart.Flower || parts.Peek() == PlantPart.Seed)
                flowerProbability = 0;

            if (IsGrowToFlower(flowerProbability))
            {
                parts.Pop();
                parts.Push(PlantPart.Flower);
            }
            else
            {
                parts.Push(PlantPart.Sqrout);
            }
            return this;
        }

        private static bool IsGrowToFlower(double flowerProbability)
        {
            var value = random.NextDouble();
            return flowerProbability > value;
        }

        protected bool Equals(Plant other)
        {
            return other.Parts.SequenceEqual(Parts);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            return obj.GetType() == GetType() && Equals((Plant) obj);
        }

        public override int GetHashCode()
        {
            return parts?.GetHashCode() ?? 0;
        }
    }
}