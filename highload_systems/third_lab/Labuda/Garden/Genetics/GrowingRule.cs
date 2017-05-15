using Garden.Flowerbed.Model;

namespace Garden.Genetics
{
    public class GrowingRule : IGrowingRule
    {
        private readonly IGrowingCondition condition;
        private readonly IGrowingAction action;

        public GrowingRule(IGrowingCondition condition, IGrowingAction action)
        {
            this.condition = condition;
            this.action = action;
        }

        public bool CanExecute(Dna dna, Plant plant)
        {
            return condition.CanExecute(dna, plant);
        }

        public void Execute(Plant plant)
        {
            action.Execute(plant);
        }
    }
}