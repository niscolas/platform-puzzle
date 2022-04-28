using MiddleMast.GameplayFramework;
using UnityEngine;

namespace PlatformPuzzle.Gameplay
{
    internal class MatchItemMB : Entity
    {
        [field: SerializeField]
        public MatchItemTypeSO Type { get; private set; }

        [field: SerializeField]
        public bool IsEnabled { get; private set; }

        public bool CompareWith(MatchItemMB other)
        {
            bool result = Type == other.Type;

            return result;
        }

        public void SetEnabled(bool value)
        {
            IsEnabled = value;
        }
    }
}

