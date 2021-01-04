using System.Collections.Generic;
using Entitas;
using Game.ECS.Components.MainMenu;
using UnityEngine.SceneManagement;

namespace Game.ECS.Systems.Menu
{
    public sealed class MainMenuSystem : ReactiveSystem<GameEntity>
    {
        public MainMenuSystem(Contexts contexts) : base(contexts.game)
        {
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
                    case MenuItemType.Play:
                        SceneManager.LoadScene("GameScene");
                        break;
                }
                
                entity.isDestroyEntity = true;
            }
        }
    }
}