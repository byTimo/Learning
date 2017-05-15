using System.Collections.Generic;
using Garden.Flowerbed.Model;

namespace Garden.Utility.Drawing
{
    public interface IDrawer
    {
        void Draw(IEnumerable<IGrowable> plants);
    }
}