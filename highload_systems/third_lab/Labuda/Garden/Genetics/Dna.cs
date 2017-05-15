using System.Linq;
using Garden.Flowerbed.Model;

namespace Garden.Genetics
{
    public class Dna
    {
        private readonly IGrowingRule[] rules;

        public Dna(IGrowingRule[] rules, int length)
        {
            this.rules = rules;
            MaxLength = length;
        }

        public bool IsGrowed { get; private set; }
        public int MaxLength { get; }


        public IGrowingRule GetGrowingRule(Plant plant)
        {
            if (IsGrowed)
                return null;

            var rule =rules.FirstOrDefault(x => x.CanExecute(this, plant));
            IsGrowed = rule == null;

            return rule;
        }

    }
}