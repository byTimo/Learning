using System;
using System.Collections.Generic;
using Garden.Flowerbed.Model;

namespace Garden.Genetics
{
    public class GrowingActionBuilder
    {
        private readonly IList<Action<Plant>> actions = new List<Action<Plant>>();

        public GrowingActionBuilder Then(Action<Plant> action)
        {
            actions.Add(action);
            return this;
        }

        public IGrowingAction Build()
        {
            return new GrowingAction(actions);
        }
    }
}