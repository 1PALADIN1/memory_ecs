using Entitas;

namespace Game.ECS.Components
{
    [Game]
    public sealed class CardComponent : IComponent
    {
        public int id;
        public int typeId;
    }
}