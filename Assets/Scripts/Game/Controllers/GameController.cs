using Game.ECS.Features;
using Game.SO;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Controllers
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private Transform _cardRoot;
        [SerializeField] private CardLibrary _cardLibrary;
        [SerializeField] private GlobalEvents _globalEvents;

        private GameFeature _gameFeature;
        private Contexts _contexts;
        private bool _needRestartLevel;

        private void Awake()
        {
            _contexts = Contexts.sharedInstance;
            _gameFeature = new GameFeature(_contexts);
            _needRestartLevel = false;
            
            CreateCardLibraryEntity();
            CreateCardRootEntity();
            CreateGlobalEventsEntity();

            _gameFeature.Initialize();
            
            _globalEvents.RestartLevelCalled += NeedRestartLevel;
        }

        private void OnDestroy()
        {
            _globalEvents.RestartLevelCalled -= NeedRestartLevel;
        }

        private void Update()
        {
            _gameFeature.Execute();
        }

        private void LateUpdate()
        {
            if (!_needRestartLevel)
                return;

            RestartLevel();
            _needRestartLevel = true;
        }

        private void CreateCardLibraryEntity()
        {
            var entity = _contexts.game.CreateEntity();
            entity.AddCardLibrary(_cardLibrary);
        }

        private void CreateCardRootEntity()
        {
            var entity = _contexts.game.CreateEntity();
            entity.AddCardRoot(_cardRoot);
        }

        private void CreateGlobalEventsEntity()
        {
            var entity = _contexts.game.CreateEntity();
            entity.AddGlobalEvents(_globalEvents);
        }

        private void NeedRestartLevel()
        {
            _needRestartLevel = true;
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
