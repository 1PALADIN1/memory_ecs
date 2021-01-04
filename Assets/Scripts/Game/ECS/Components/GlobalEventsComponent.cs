using Entitas;
using Entitas.CodeGeneration.Attributes;
using Game.SO;

namespace Game.ECS.Components
{
    [Game, Unique]
    public sealed class GlobalEventsComponent : IComponent
    {
        public GlobalEvents value;
    }
}