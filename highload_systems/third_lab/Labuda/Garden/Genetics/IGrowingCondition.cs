using Garden.Flowerbed.Model;

namespace Garden.Genetics
{
    public interface IGrowingCondition
    {
        bool CanExecute(Dna dna, Plant plant);
    }
}