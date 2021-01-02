using Game.ECS.Components.MainMenu;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Views
{
    [RequireComponent(typeof(Button))]
    public class MainMenuButton : MonoBehaviour
    {
        [SerializeField] private MenuItemType _menuItemType;

        private Contexts _contexts;
        private Button _button;
        
        private void Awake()
        {
            _contexts = Contexts.sharedInstance;
            _button = GetComponent<Button>();
            
            _button
                .onClick
                .AddListener(OnButtonClicked);
        }

        private void OnDestroy()
        {
            _button
                .onClick
                .RemoveListener(OnButtonClicked);
        }

        private void OnButtonClicked()
        {
            var menuClickEntity = _contexts.game.CreateEntity();
            menuClickEntity.AddSelectMenuItem(_menuItemType);
        }
    }
}