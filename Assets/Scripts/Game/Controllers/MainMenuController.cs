using Game.ECS.Features;
using UnityEngine;

namespace Game.Controllers
{
    public class MainMenuController : MonoBehaviour
    {
        private MainMenuFeature _mainMenuFeature;
        private Contexts _contexts;
        
        private void Awake()
        {
            _contexts = Contexts.sharedInstance;
            _mainMenuFeature = new MainMenuFeature(_contexts);
            
            _mainMenuFeature.Initialize();
        }

        private void Update()
        {
            _mainMenuFeature.Execute();
        }
    }
}