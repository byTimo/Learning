using System.Collections.Generic;
using System.Linq;
using Garden.Genetics;

namespace Garden.Flowerbed
{
    public class Flower : IGrowable
    {
        private readonly Dna dna;
        private readonly IList<PlantSegment> segments;

        public Flower() : this(new Dna(2)) {  }

        public Flower(Dna dna) :this(dna, new []{PlantSegment.Seed}) { }

        private Flower(Dna dna, IEnumerable<PlantSegment> segments)
        {
            this.dna = dna;
            this.segments = new List<PlantSegment>(segments);
        }

        public PlantSegment[] Segments => segments.ToArray();

        public IGrowable Grow()
        {
            if (segments.Count - 1 < dna.Length)
            {
                segments.Add(segments.Last() == PlantSegment.Sqrout ? PlantSegment.Flower : PlantSegment.Sqrout);
            }
            return this;
        }

        public IGrowable Clone()
        {
            return new Flower(dna, Segments);
        }
    }
}