using Game.Views;
using UnityEngine;

namespace Game.SO
{
    [CreateAssetMenu(fileName = "CardLibrary", menuName = "SO/Card Library", order = 1)]
    public class CardLibrary : ScriptableObject
    {
        [SerializeField] private CardView[] _cardPrefabs;

        public CardView[] CardPrefabs => _cardPrefabs;
    }
}