using Entitas;
using UnityEngine;

namespace Game.ECS.Systems
{
    public class DelayedActionSystem : IExecuteSystem
    {
        private readonly Contexts _contexts;
        private readonly IGroup<GameEntity> _delayedActions;
        
        public DelayedActionSystem(Contexts contexts)
        {
            _contexts = contexts;
            _delayedActions = _contexts.game.GetGroup(GameMatcher.DelayedAction);
        }
        
        public void Execute()
        {
            foreach (var entity in _delayedActions.GetEntities())
            {
                var timeLeft = entity.delayedAction.delay - Time.deltaTime;
                entity.ReplaceDelayedAction(timeLeft, entity.delayedAction.action);
                
                if (timeLeft <= 0f)
                {
                    entity.delayedAction.action?.Invoke();
                    entity.RemoveDelayedAction();
                    entity.isDestroyEntity = true;
                }
            }
        }
    }
}