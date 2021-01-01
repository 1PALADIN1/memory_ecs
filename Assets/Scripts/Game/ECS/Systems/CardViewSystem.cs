using System.Collections.Generic;
using Entitas;

namespace Game.ECS.Systems
{
    public class CardViewSystem : ReactiveSystem<GameEntity>
    {
        public CardViewSystem(Contexts contexts) : base(contexts.game)
        {
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(
                GameMatcher.AllOf(GameMatcher.CardView, GameMatcher.OpenedCard));
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.hasCardView && entity.hasOpenedCard;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var entity in entities)
            {
                if (entity.openedCard.value)
                    entity.cardView.value.Open();
            }
        }
    }
}