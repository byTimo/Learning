using System;
using Garden.Flowerbed.Model;

namespace Garden.Genetics
{
    public class SmallSheetGrowingRule : IGrowingRule
    {
        private readonly Func<Dna, Plant, int> getHeight;
        private readonly Func<Dna, Plant, int> getShift;

        private int currentHeight;
        private int currentShift;
        private int growingPhase;

        public SmallSheetGrowingRule(Func<Dna, Plant, int> getHeight, Func<Dna, Plant, int> getShift)
        {
            this.getHeight = getHeight;
            this.getShift = getShift;
        }

        public bool CanExecute(Dna dna, Plant plant)
        {
            currentHeight = getHeight.Invoke(dna, plant);
            currentShift = getShift.Invoke(dna, plant);
            return growingPhase < 3 && plant.IsInPosition(PlantSegment.Sheet, currentHeight, currentShift);
        }

        public void Execute(Plant plant)
        {
            switch (growingPhase)
            {
                case 0:
                    plant.Add(PlantSegment.Sheet, currentHeight - 1, currentShift);
                    break;
                case 1:
                    plant.Add(PlantSegment.Sheet, currentHeight, currentShift - 1);
                    plant.Add(PlantSegment.Sheet, currentHeight, currentShift + 1);
                    break;
                case 2:
                    plant.Add(PlantSegment.Sheet, currentHeight + 1, currentShift);
                    break;
            }
            growingPhase++;
        }
    }
}