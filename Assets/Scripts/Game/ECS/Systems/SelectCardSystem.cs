using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Game.ECS.Systems
{
    public sealed class SelectCardSystem : ReactiveSystem<GameEntity>
    {
        private const int InitId = -1;

        private SelectedCards _selectedCards;
        
        public SelectCardSystem(Contexts contexts) : base(contexts.game)
        {
            _selectedCards = new SelectedCards
            {
                FirstCardId = InitId,
                SecondCardId = InitId
            };
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.SelectCard);
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.hasSelectCard;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var entity in entities)
            {
                Debug.Log($"Selected: {entity.selectCard.id}");
                TryAddCard(entity.selectCard.id);
                entity.isDestroyEntity = true;
            }
        }

         private void TryAddCard(int id)
        {
            //open first card
            if (_selectedCards.FirstCardId == InitId)
            {
                _selectedCards.FirstCardId = id;
                return;
            }
            
            //open second card
            _selectedCards.SecondCardId = id;
        }
        
        private struct SelectedCards
        {
            public int FirstCardId { get; set; }
            public int SecondCardId { get; set; }
        }
    }
}