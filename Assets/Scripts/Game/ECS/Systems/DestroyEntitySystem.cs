using System.Collections.Generic;
using Entitas;

namespace Game.ECS.Systems
{
    public sealed class DestroyEntitySystem : ReactiveSystem<GameEntity>
    {
        public DestroyEntitySystem(Contexts contexts) : base(contexts.game)
        {
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.DestroyEntity);
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.isDestroyEntity;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var entity in entities)
                entity.Destroy();
        }
    }
}