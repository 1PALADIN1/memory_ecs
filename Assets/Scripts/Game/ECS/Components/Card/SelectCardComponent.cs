using Entitas;

namespace Game.ECS.Components.Card
{
    [Game]
    public sealed class SelectCardComponent : IComponent
    {
        public int id;
        public int typeId;
    }
}