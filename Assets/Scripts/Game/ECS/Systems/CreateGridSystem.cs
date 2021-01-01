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

            var id = 0;
            for (int i = 0; i < CardsAmount; i++)
            {
                CreateCardEntity(cardPrefabs[i], i, id);
                id++;
                
                CreateCardEntity(cardPrefabs[i], i, id);
                id++;
            }
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
    }
}