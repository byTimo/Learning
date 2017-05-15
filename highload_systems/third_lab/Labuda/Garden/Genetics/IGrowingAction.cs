using Garden.Flowerbed.Model;

namespace Garden.Genetics
{
    public interface IGrowingAction
    {
        void Execute(Plant plant);
    }
}