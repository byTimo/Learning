using System.Collections.Generic;
using Garden.Flowerbed;

namespace Garden.Drawing
{
    public interface IDrawer
    {
        void Draw(IEnumerable<IGrowable> plants);
    }
}