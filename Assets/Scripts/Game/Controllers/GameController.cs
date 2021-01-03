using System;
using Game.ECS.Features;
using Game.SO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game.Controllers
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private Transform _cardRoot;
        [SerializeField] private CardLibrary _cardLibrary;
        [SerializeField] private Button _restartButton;
        
        private GameFeature _gameFeature;
        private Contexts _contexts;

        private void Awake()
        {
            _contexts = Contexts.sharedInstance;
            _gameFeature = new GameFeature(_contexts);
            
            CreateCardLibraryEntity();
            CreateCardRootEntity();

            _gameFeature.Initialize();
            
            _restartButton
                .onClick
                .AddListener(RestartLevel);
        }

        private void OnDestroy()
        {
            _restartButton
                .onClick
                .RemoveListener(RestartLevel);
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

        private void RestartLevel()
        {
            foreach (var context in _contexts.allContexts)
            {
                context.DestroyAllEntities();
                context.ClearComponentPools();
            }
            
            _gameFeature.ClearReactiveSystems();

            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
