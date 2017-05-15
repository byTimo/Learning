using System;
using System.Collections.Generic;
using System.Linq;
using Garden.Flowerbed.Model;

namespace Garden.Genetics
{
    public class GrowingCondition : IGrowingCondition
    {
        private readonly IEnumerable<Func<Dna, Plant, bool>> conditions;

        public GrowingCondition(IEnumerable<Func<Dna, Plant, bool>> conditions)
        {
            this.conditions = conditions.ToArray();
        }

        public bool CanExecute(Dna dna, Plant plant)
        {
            return conditions.All(x => x.Invoke(dna, plant));
        }
    }
}