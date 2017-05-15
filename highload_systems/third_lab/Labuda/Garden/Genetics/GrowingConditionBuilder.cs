using System;
using System.Collections.Generic;
using Garden.Flowerbed.Model;

namespace Garden.Genetics
{
    public class GrowingConditionBuilder
    {
        private readonly IList<Func<Dna, Plant, bool>> conditions = new List<Func<Dna, Plant, bool>>();

        public GrowingConditionBuilder WhenNotMaxLength()
        {
            return When((d, p) => p.Top < d.MaxLength);
        }

        public GrowingConditionBuilder WhenMaxLength()
        {
            return When((d, p) => p.Top >= d.MaxLength);
        }

        public GrowingConditionBuilder WhenLast(PlantSegment segment)
        {
            return When((d, p) => p.Last == segment);
        }

        public GrowingConditionBuilder When(Func<Dna, Plant, bool> condition)
        {
            conditions.Add(condition);
            return this;
        }

        public GrowingConditionBuilder When(Func<Plant, bool> condition)
        {
            conditions.Add((d, p) => condition(p));
            return this;
        }

        public IGrowingCondition Build()
        {
            return new GrowingCondition(conditions);
        }
    }
}