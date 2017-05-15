using System.Collections.Generic;
using Garden.Flowerbed.Model;
using Garden.Genetics;
using Garden.Utility;

namespace Garden.Flowerbed
{
    public class Shrub : Plant
    {
        public Shrub() : this(new[] {PlantSegment.Seed})
        {
        }

        private Shrub(IEnumerable<PlantSegment> segments) : base(segments)
        {
        }

        public override int GrowingTime => RandomHelper.Between(100, 3000);


        protected override Dna CreateDna()
        {
            return new DnaBuilder()
                .AddGrowingRule(c => c.WhenNotMaxLength()
                                    .WhenLast(PlantSegment.Seed),
                                a => a.Then(t => t.Add(PlantSegment.Sqrout)))
                .AddGrowingRule(c => c.WhenNotMaxLength()
                                    .WhenLast(PlantSegment.Sqrout),
                                a => a.Then(t => t.SwapLast(PlantSegment.Wood))
                                    .Then(t => t.Add(PlantSegment.Sqrout)))
                .AddGrowingRule(c => c.WhenMaxLength()
                                    .WhenLast(PlantSegment.Sqrout),
                                a => a.Then(t => t.SwapLast(PlantSegment.Wood))
                                    .Then(t => t.Add(PlantSegment.Sheet)))
                .AddSmallSheetGrowingRule((d, p) => d.MaxLength, (d, p) => 0)
                .SetMaxLegth(3)
                .Build();
        }
    }
}