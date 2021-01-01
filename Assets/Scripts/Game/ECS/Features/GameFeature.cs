using Game.ECS.Systems;
using Game.ECS.Systems.Grid;

namespace Game.ECS.Features
{
    public sealed class GameFeature : Feature
    {
        public GameFeature(Contexts contexts)
        {
            //init
            Add(new CreateGridSystem(contexts));
            
            //logic
            Add(new SelectCardSystem(contexts));
            Add(new DelayedActionSystem(contexts));
            
            //render
            Add(new CardViewSystem(contexts));
            
            //clean up
            Add(new DestroyEntitySystem(contexts));
        }
    }
}
