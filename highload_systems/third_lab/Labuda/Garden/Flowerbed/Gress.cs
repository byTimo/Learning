using System.Collections.Generic;
using System.Linq;
using Garden.Genetics;

namespace Garden.Flowerbed
{
    public class Gress : IGrowable
    {
        private readonly Dna dna;
        private readonly IList<PlantSegment> segments;

        public Gress() : this(new Dna(1))
        {
        }

        public Gress(Dna dna) : this(dna, new[] {PlantSegment.Seed})
        {
        }

        private Gress(Dna dna, IEnumerable<PlantSegment> segments)
        {
            this.dna = dna;
            this.segments = new List<PlantSegment>(segments);
        }

        public PlantSegment[] Segments => segments.ToArray();

        public IGrowable Grow()
        {
            if (segments.Count - 1 < dna.Length)
                segments.Add(PlantSegment.Sqrout);
            return this;
        }

        public IGrowable Clone()
        {
            return new Gress(dna, Segments);
        }
    }
}