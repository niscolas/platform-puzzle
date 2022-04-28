using MiddleMast.GameplayFramework;
using UnityEngine;
using UnityEngine.Events;

namespace PlatformPuzzle.Gameplay
{
    internal class MatchItemMB : Entity
    {
        [field: SerializeField]
        public MatchItemTypeSO Type { get; private set; }

        [field: SerializeField]
        public bool IsEnabled { get; private set; }

        [Header("Events")]
        [SerializeField]
        private UnityEvent _onMatch;

        public bool CompareWith(MatchItemMB other)
        {
            bool result = Type == other.Type;

            return result;
        }

        public void OnMatch()
        {
            IsEnabled = false;
            _onMatch?.Invoke();
            gameObject.SetActive(false);
        }

        public void SetEnabled(bool value)
        {
            IsEnabled = value;
        }
    }
}

