using System;
using UnityEngine;

namespace PlatformPuzzle.Gameplay
{
    [Serializable]
    internal class DirectionedMatchItem
    {
        [field: SerializeField]
        public Direction Direction { get; private set; }

        [field: SerializeField]
        public MatchItemMB MatchItem { get; private set; }

        public void SetDirection(Direction direction)
        {
            Direction = direction;
        }

        public void SetMatchItem(MatchItemMB matchItem)
        {
            MatchItem = matchItem;
        }
    }
}
