using UnityEngine;

namespace Game.SO
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "SO/Game Settings", order = 3)]
    public sealed class GameSettings : ScriptableObject
    {
        [SerializeField] private int _targetFrameRate;

        public int TargetFrameRate => _targetFrameRate;
    }
}