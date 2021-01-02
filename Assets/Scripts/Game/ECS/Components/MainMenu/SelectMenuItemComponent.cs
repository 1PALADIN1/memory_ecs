using Entitas;

namespace Game.ECS.Components.MainMenu
{
    [Game]
    public sealed class SelectMenuItemComponent : IComponent
    {
        public MenuItemType value;
    }
}