using System;
using System.Collections.Generic;
using System.Linq;
using Garden.Flowerbed.Model;

namespace Garden.Genetics
{
    public class GrowingAction : IGrowingAction
    {
        private readonly Action<Plant>[] actions;

        public GrowingAction(IEnumerable<Action<Plant>> actions)
        {
            this.actions = actions.ToArray();
        }

        public void Execute(Plant plant)
        {
            foreach (var action in actions)
            {
                action.Invoke(plant);
            }
        }
    }
}