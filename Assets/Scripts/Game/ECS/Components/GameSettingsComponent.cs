using Entitas;
using Entitas.CodeGeneration.Attributes;
using Game.SO;

namespace Game.ECS.Components
{
    [Game, Unique]
    public sealed class GameSettingsComponent : IComponent
    {
        public GameSettings value;
    }
}