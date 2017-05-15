using System.Collections.Generic;
using Garden.Flowerbed.Model;
using Garden.Genetics;

namespace Garden.Flowerbed
{
    public class Gress : Plant
    {
        public Gress()
        {
        }

        public Gress(IEnumerable<PlantSegment> segments) : base(segments)
        {
        }

        protected override Dna CreateDna()
        {
            return new DnaBuilder()
                .AddGrowingRule(c => c.WhenNotMaxLength(),
                                a => a.Then(t => t.Add(PlantSegment.Sqrout)))
                .SetMaxLegth(2)
                .Build();
        }
    }
}