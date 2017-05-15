using System;
using System.Collections.Generic;
using System.Linq;
using Garden.Flowerbed.Model;

namespace Garden.Genetics
{
    public class DnaBuilder
    {
        private readonly IList<IGrowingRule> growingRule = new List<IGrowingRule>();
        private int maxLength;

        public DnaBuilder AddGrowingRule(Func<GrowingConditionBuilder, GrowingConditionBuilder> condition, Func<GrowingActionBuilder, GrowingActionBuilder> action)
        {
            growingRule.Add(new GrowingRule(condition.Invoke(new GrowingConditionBuilder()).Build(),
                                            action.Invoke(new GrowingActionBuilder()).Build()));
            return this;
        }

        public DnaBuilder AddSmallSheetGrowingRule(Func<Dna, Plant, int> getHeight, Func<Dna, Plant, int> getShift)
        {
            growingRule.Add(new SmallSheetGrowingRule(getHeight, getShift));
            return this;
        }

        public DnaBuilder AddSmallSheetGrowingRule(int height, int shift)
        {
            return AddSmallSheetGrowingRule((d, p) => height, (d, p) => shift);
        }

        public DnaBuilder SetMaxLegth(int length)
        {
            maxLength = length;
            return this;
        }

        public Dna Build()
        {
            return new Dna(growingRule.ToArray(), maxLength);
        }
    }
}