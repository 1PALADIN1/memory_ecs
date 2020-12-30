using Entitas;
using Entitas.CodeGeneration.Attributes;
using Game.SO;

namespace Game.ECS.Components.Card
{
    [Game, Unique]
    public class CardLibraryComponent : IComponent
    {
        public CardLibrary value;
    }
}