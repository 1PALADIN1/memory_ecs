using Entitas;
using Game.Views;

namespace Game.ECS.Components.Card
{
    [Game]
    public sealed class CardViewComponent : IComponent
    {
        public CardView value;
    }
}