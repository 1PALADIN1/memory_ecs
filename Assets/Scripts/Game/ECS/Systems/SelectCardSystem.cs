using System;
using System.Collections.Generic;
using System.Linq;
using Entitas;

namespace Game.ECS.Systems
{
    public sealed class SelectCardSystem : ReactiveSystem<GameEntity>
    {
        private const int InitTypeId = -1;

        private readonly IGroup<GameEntity> _cardsGroup;
        
        private SelectedCards _selectedCards;

        public SelectCardSystem(Contexts contexts) : base(contexts.game)
        {
            _selectedCards = new SelectedCards
            {
                FirstCardTypeId = InitTypeId,
                SecondCardTypeId = InitTypeId
            };

            _cardsGroup = contexts.game.GetGroup(
                GameMatcher.AllOf(GameMatcher.Card, GameMatcher.OpenedCard));
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
                TrySelectCard(entity.selectCard.id, entity.selectCard.typeId);
                entity.isDestroyEntity = true;
            }
        }
        
        private void TrySelectCard(int id, int typeId)
        {
            //open first card
            if (_selectedCards.FirstCardTypeId == InitTypeId)
            {
                OpenCard(id, typeId);
                _selectedCards.FirstCardTypeId = typeId;
                return;
            }
            
            //open second card
            if (_selectedCards.SecondCardTypeId == InitTypeId)
            {
                _selectedCards.SecondCardTypeId = typeId;
                OpenCard(id, typeId);
            }
        }

        private void OpenCard(int id, int typeId)
        {
            foreach (var cardEntity in _cardsGroup.GetEntities()
                .Where(c => c.card.id == id && c.card.typeId == typeId))
            {
                cardEntity.ReplaceOpenedCard(true);
            }
        }
        
        private struct SelectedCards
        {
            public int FirstCardTypeId { get; set; }
            public int SecondCardTypeId { get; set; }
        }
    }
}