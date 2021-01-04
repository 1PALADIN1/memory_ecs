using Entitas;
using UnityEngine;

namespace Game.ECS.Systems
{
    public sealed class SettingsInitSystem : IInitializeSystem
    {
        private readonly Contexts _contexts;
        
        public SettingsInitSystem(Contexts contexts)
        {
            _contexts = contexts;
        }
        
        public void Initialize()
        {
            var gameSettings = _contexts.game.gameSettings.value;
            Application.targetFrameRate = gameSettings.TargetFrameRate;
        }
    }
}