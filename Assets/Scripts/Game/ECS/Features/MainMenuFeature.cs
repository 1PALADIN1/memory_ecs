using Game.ECS.Systems;
using Game.ECS.Systems.MainMenu;

namespace Game.ECS.Features
{
    public sealed class MainMenuFeature : Feature
    {
        public MainMenuFeature(Contexts contexts)
        {
            Add(new MainMenuSystem(contexts));
            
            //cleanup
            Add(new DestroyEntitySystem(contexts));
        }
    }
}