using Entitas;

namespace Game.ECS.Systems.Grid
{
    public sealed class CreateGridSystem : IInitializeSystem
    {
        private readonly Contexts _contexts;
        
        public CreateGridSystem(Contexts contexts)
        {
            _contexts = contexts;
        }
        
        public void Initialize()
        {
            var gridEntity = _contexts.game.CreateEntity();
            gridEntity.AddGrid(3, 2, 3);
            //TODO: create cards
        }
    }
}