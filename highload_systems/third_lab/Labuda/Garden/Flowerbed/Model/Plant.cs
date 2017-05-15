using System;
using System.Collections.Generic;
using System.Linq;
using Garden.Genetics;
using Garden.Utility;

namespace Garden.Flowerbed.Model
{
    public abstract class Plant : IGrowable
    {
        protected readonly Dna Dna;
        private readonly Dictionary<GrowingPosition, PlantSegment> segments;

        protected Plant() : this(new[] {PlantSegment.Seed})
        {
        }

        protected Plant(IEnumerable<PlantSegment> segments)
        {
            Dna = CreateDna();
            this.segments =
                segments.Select((x, i) => Tuple.Create(i, x))
                    .ToDictionary(x => GrowingPosition.Create(x.Item1, 0), x => x.Item2);
            Top = this.segments.Count;
        }

        public virtual int GrowingTime => RandomHelper.Between(100, 2000);
        public PlantSegment[] Segments => segments.Values.ToArray();

        public KeyValuePair<GrowingPosition, PlantSegment>[] Nodes => segments.ToArray();

        public int Top { get; private set; }
        public bool IsGrowed => Dna.IsGrowed;
        public PlantSegment Last => segments[GrowingPosition.Create(Top -1, 0)];

        public virtual IGrowable Grow()
        {
            var rule = Dna.GetGrowingRule(this);
            rule?.Execute(this);
            return this;
        }

        protected abstract Dna CreateDna();

        public Plant Add(PlantSegment segment) => Add(segment, Top, 0);

        public Plant Add(PlantSegment segment, int height, int shift)
        {
            segments[GrowingPosition.Create(height, shift)] = segment;
            if (Top <= height)
            {
                Top = segments.Count;
            }
            return this;
        }

        public Plant SwapLast(PlantSegment segment) => Swap(segment, Top - 1, 0);

        public Plant SwapLast(PlantSegment segmnet, PlantSegment swapingSegment)
        {
            var lastPosition = segments.Last(x => x.Value == segmnet).Key;
            return Swap(segmnet, lastPosition.Height, lastPosition.Shift);
        }

        public Plant Swap(PlantSegment segment, int height, int shift)
        {
            segments[GrowingPosition.Create(height, shift)] = segment;
            return this;
        }

        public int Count(PlantSegment segment)
        {
            return segments.Count(x => x.Value == segment);
        }

        public bool IsInPosition(PlantSegment segment, int height, int shift)
        {
            return segments.Where(x => x.Key.Equals(GrowingPosition.Create(height, shift)))
                .Any(x => x.Value == segment);
        }
    }
}