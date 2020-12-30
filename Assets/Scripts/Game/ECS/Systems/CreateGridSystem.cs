using Entitas;
using Game.Views;
using UnityEngine;

namespace Game.ECS.Systems.Grid
{
    public sealed class CreateGridSystem : IInitializeSystem
    {
        private const int CardsAmount = 3;
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
            for (int i = 0; i < CardsAmount; i++)
            {
                CreateCardEntity(cardPrefabs[i], i);
                CreateCardEntity(cardPrefabs[i], i);
            }
        }

        private void CreateCardEntity(CardView cardPrefab, int id)
        {
            var cardView = GameObject.Instantiate(cardPrefab, _contexts.game.cardRoot.value);
            cardView.Init(id);
            
            var cardEntity = _contexts.game.CreateEntity();
            cardEntity.AddCardView(cardView);
            cardEntity.AddCard(id);
        }
    }
}