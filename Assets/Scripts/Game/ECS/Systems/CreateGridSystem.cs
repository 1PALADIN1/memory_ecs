using System.Collections.Generic;
using System.Linq;
using Entitas;
using Game.Views;
using UnityEngine;

namespace Game.ECS.Systems
{
    public sealed class CreateGridSystem : IInitializeSystem
    {
        private const int CardsAmount = 9;
        private const int Rows = 2;
        private const int Columns = 3;

        private readonly Contexts _contexts;

        public CreateGridSystem(Contexts contexts)
        {
            _contexts = contexts;
        }
        
        public void Initialize()
        {
            var gridEntity = _contexts.game.CreateEntity();
            gridEntity.AddGrid(CardsAmount, Rows, Columns);

            var cardLibrary = _contexts.game.cardLibrary.value;
            var cardPrefabs = cardLibrary.CardPrefabs;
            var cardsList = new List<Card>();

            var id = 0;
            for (int i = 0; i < CardsAmount; i++)
            {
                cardsList.Add(new Card
                {
                    Id = id,
                    TypeId = i,
                    CardPrefab = cardPrefabs[i]
                });

                id++;
                
                cardsList.Add(new Card
                {
                    Id = id,
                    TypeId = i,
                    CardPrefab = cardPrefabs[i]
                });

                id++;
            }

            var iterations = cardsList.Count;
            for (int i = 0; i < iterations; i++)
            {
                var randIndex = cardsList.Count == 1 ? 0
                    : Random.Range(0, cardsList.Count);
                CreateCardEntity(cardsList[randIndex]);
                
                cardsList.RemoveAt(randIndex);
            }
        }

        private void CreateCardEntity(Card card)
        {
            CreateCardEntity(card.CardPrefab, card.TypeId, card.Id);
        }

        private void CreateCardEntity(CardView cardPrefab, int typeId, int id)
        {
            var cardView = GameObject.Instantiate(cardPrefab, _contexts.game.cardRoot.value);
            cardView.Init(id, typeId);
            
            var cardEntity = _contexts.game.CreateEntity();
            cardEntity.AddCardView(cardView);
            cardEntity.AddCard(id, typeId);
            cardEntity.AddOpenedCard(false);
        }

        private struct Card
        {
            public int Id { get; set; }
            public int TypeId { get; set; }
            public CardView CardPrefab { get; set; }
        }
    }
}