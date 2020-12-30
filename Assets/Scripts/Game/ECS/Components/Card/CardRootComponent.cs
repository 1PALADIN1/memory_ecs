using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

namespace Game.ECS.Components.Card
{
    [Game, Unique]
    public sealed class CardRootComponent : IComponent
    {
        public Transform value;
    }
}