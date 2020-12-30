using Game.ECS.Features;
using Game.SO;
using UnityEngine;

namespace Game.Controllers
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private Transform _cardRoot;
        [SerializeField] private CardLibrary _cardLibrary;
        
        private GameFeature _gameFeature;
        private Contexts _contexts;

        private void Awake()
        {
            _contexts = Contexts.sharedInstance;
            _gameFeature = new GameFeature(_contexts);
            
            CreateCardLibraryEntity();
            CreateCardRootEntity();

            _gameFeature.Initialize();
        }

        private void Update()
        {
            _gameFeature.Execute();
        }

        private void CreateCardLibraryEntity()
        {
            var cardLibraryEntity = _contexts.game.CreateEntity();
            cardLibraryEntity.AddCardLibrary(_cardLibrary);
        }

        private void CreateCardRootEntity()
        {
            var cardRootEntity = _contexts.game.CreateEntity();
            cardRootEntity.AddCardRoot(_cardRoot);
        }
    }
}
