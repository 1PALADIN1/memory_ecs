using Game.ECS.Systems;
using Game.ECS.Systems.Menu;

namespace Game.ECS.Features
{
    public sealed class MainMenuFeature : Feature
    {
        public MainMenuFeature(Contexts contexts)
        {
            //init
            Add(new SettingsInitSystem(contexts));
            
            //render
            Add(new MainMenuSystem(contexts));
            
            //cleanup
            Add(new DestroyEntitySystem(contexts));
        }
    }
}