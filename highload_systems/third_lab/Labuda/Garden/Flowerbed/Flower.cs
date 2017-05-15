using System.Collections.Generic;
using Garden.Flowerbed.Model;
using Garden.Genetics;

namespace Garden.Flowerbed
{
    public class Flower : Plant
    {
        public Flower() { }

        public Flower(IEnumerable<PlantSegment> segments) : base(segments)
        {
        }

        protected override Dna CreateDna()
        {
            return new DnaBuilder()
                .AddGrowingRule(c => c.WhenNotMaxLength(),
                                a => a.Then(t => t.Add(PlantSegment.Sqrout)))
                .AddGrowingRule(c => c.WhenLast(PlantSegment.Sqrout),
                                a => a.Then(t => t.Add(PlantSegment.Flower)))
                .SetMaxLegth(2)
                .Build();
        }
    }
}