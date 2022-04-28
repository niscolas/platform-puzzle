using System;
using UnityEngine;

namespace PlatformPuzzle.Gameplay
{
    [Serializable]
    internal class MatchItemWithDirection
    {
        [field: SerializeField]
        public Direction Direction { get; private set; }

        [field: SerializeField]
        public MatchItemMB MatchItem { get; private set; }

        public MatchItemWithDirection(Direction direction, MatchItemMB matchItem)
        {
            Direction = direction;
            MatchItem = matchItem;
        }
    }
}


