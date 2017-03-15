using System.Collections.Generic;
using Garden.Flowerbed;

namespace Garden
{
    internal interface IDrawer
    {
        void Draw(IDictionary<int, Plant> plants);
    }
}