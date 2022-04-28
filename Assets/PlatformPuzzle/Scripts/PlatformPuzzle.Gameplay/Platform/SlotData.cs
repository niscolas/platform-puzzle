using System;
using UnityEngine;

namespace PlatformPuzzle.Gameplay
{
    [Serializable]
    internal class SlotData
    {
        [field: SerializeField]
        public Direction Direction { get; private set; }

        [field: SerializeField]
        public SnapPoint SnapPoint { get; private set; }

        [field: SerializeField]
        public MatchItemMB MatchItem { get; private set; }

        public SlotData() { }

        public SlotData(
                Direction direction = default,
                SnapPoint snapPoint = default,
                MatchItemMB matchItem = default)
        {
            Direction = direction;
            SnapPoint = snapPoint;
            MatchItem = matchItem;
        }

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

