using System.Collections.Generic;
using Entitas;
using Game.ECS.Components.MainMenu;

namespace Game.ECS.Systems.Menu
{
    public sealed class GameMenuSystem : ReactiveSystem<GameEntity>
    {
        private readonly Contexts _contexts;
        
        public GameMenuSystem(Contexts contexts) : base(contexts.game)
        {
            _contexts = contexts;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.SelectMenuItem);
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.hasSelectMenuItem;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var entity in entities)
            {
                switch (entity.selectMenuItem.value)
                {
                    case MenuItemType.RestartLevel:
                        _contexts.game.globalEvents.value.CallRestartLevel();
                        break;
                }
                
                entity.isDestroyEntity = true;
            }
        }
    }
}