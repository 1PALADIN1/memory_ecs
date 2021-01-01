using System;
using Entitas;

namespace Game.ECS.Components
{
    [Game]
    public sealed class DelayedActionComponent : IComponent
    {
        public float delay;
        public Action action;
    }
}