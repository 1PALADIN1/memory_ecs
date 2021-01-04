using System;
using UnityEngine;

namespace Game.SO
{
    [CreateAssetMenu(fileName = "GlobalEvents", menuName = "SO/Global Events", order = 2)]
    public sealed class GlobalEvents : ScriptableObject
    {
        public event Action RestartLevelCalled;

        public void CallRestartLevel()
        {
            RestartLevelCalled?.Invoke();
        }
    }
}