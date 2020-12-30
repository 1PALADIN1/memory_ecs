using Game.ECS.Features;
using UnityEngine;

namespace Game.Controllers
{
    public class GameController : MonoBehaviour
    {
        private GameFeature _gameFeature;
        private Contexts _contexts;
        
        private void Awake()
        {
            _contexts = Contexts.sharedInstance;
            _gameFeature = new GameFeature(_contexts);
            
            _gameFeature.Initialize();
        }

        private void Update()
        {
            _gameFeature.Execute();
        }
    }
}
