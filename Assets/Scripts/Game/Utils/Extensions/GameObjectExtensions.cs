using UnityEngine;

namespace Game.Utils.Extensions
{
    public static class GameObjectExtensions
    {
        public static void TrySetActive(this GameObject gameObject, bool isActive)
        {
            if (gameObject.activeSelf == isActive)
                return;
            
            gameObject.SetActive(isActive);
        }
    }
}