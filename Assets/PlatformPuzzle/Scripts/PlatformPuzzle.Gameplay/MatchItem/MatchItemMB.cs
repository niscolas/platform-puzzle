using System;
using System.Collections.Generic;
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

        public event Action Matched;

        public override void Setup()
        {
            SetupComponents();
        }

        public bool CompareWith(MatchItemMB other)
        {
            bool result = Type == other.Type;

            return result;
        }

        public void OnMatch()
        {
            IsEnabled = false;
            Matched?.Invoke();
            _onMatch?.Invoke();
        }

        public void SetEnabled(bool value)
        {
            IsEnabled = value;
        }

        private void SetupComponents()
        {
            IEnumerable<IMatchItemComponent> components = GetComponentsInChildren<IMatchItemComponent>();
            foreach (IMatchItemComponent component in components)
            {
                component.Setup(this);
            }
        }
    }
}

