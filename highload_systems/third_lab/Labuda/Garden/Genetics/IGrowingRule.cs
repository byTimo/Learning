using Garden.Flowerbed.Model;

namespace Garden.Genetics
{
    public interface IGrowingRule
    {
        bool CanExecute(Dna dna, Plant plant);

        void Execute(Plant plant);
    }
}