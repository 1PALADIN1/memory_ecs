using System.Collections.Generic;
using System.Linq;
using Entitas;

namespace Game.ECS.Systems
{
    public sealed class SelectCardSystem : ReactiveSystem<GameEntity>
    {
        private const int InitId = -1;

        private readonly IGroup<GameEntity> _cardsGroup;
        private readonly Contexts _contexts;
        
        private SelectedCards _selectedCards;

        private bool IsCardTypesMatch => _selectedCards.FirstCardTypeId == _selectedCards.SecondCardTypeId; 

        public SelectCardSystem(Contexts contexts) : base(contexts.game)
        {
            ResetSelectedCards();

            _contexts = contexts;
            _cardsGroup = _contexts.game.GetGroup(
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
            if (_selectedCards.FirstCardTypeId == InitId)
            {
                ChangeCardState(id, true);
                _selectedCards.FirstCardId = id;
                _selectedCards.FirstCardTypeId = typeId;
                return;
            }
            
            //open second card
            if (_selectedCards.SecondCardTypeId == InitId)
            {
                ChangeCardState(id, true);
                _selectedCards.SecondCardId = id;
                _selectedCards.SecondCardTypeId = typeId;

                _contexts.game.CreateEntity().AddDelayedAction(1f,
                    () =>
                    {
                        if (IsCardTypesMatch)
                        {
                            ResetSelectedCards();
                            return;
                        }
                        
                        ChangeCardState(_selectedCards.FirstCardId, false);
                        ChangeCardState(_selectedCards.SecondCardId, false);
                    
                        _contexts.game.CreateEntity().AddDelayedAction(0.5f, ResetSelectedCards);
                    });
            }
        }

        private void ChangeCardState(int id, bool isOpened)
        {
            foreach (var cardEntity in _cardsGroup.GetEntities()
                .Where(c => c.card.id == id))
            {
                cardEntity.ReplaceOpenedCard(isOpened);
            }
        }
        
        private void ResetSelectedCards()
        {
            _selectedCards = new SelectedCards
            {
                FirstCardId = InitId,
                FirstCardTypeId = InitId,
                SecondCardId = InitId,
                SecondCardTypeId = InitId
            };
        }

        private struct SelectedCards
        {
            public int FirstCardId { get; set; }
            public int FirstCardTypeId { get; set; }
            public int SecondCardId { get; set; }
            public int SecondCardTypeId { get; set; }
        }
    }
}