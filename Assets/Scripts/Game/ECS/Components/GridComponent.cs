using Entitas;

namespace Game.ECS.Components
{
    public sealed class GridComponent : IComponent
    {
        public int cardsAmount;
        public int rows;
        public int columns;
    }
}