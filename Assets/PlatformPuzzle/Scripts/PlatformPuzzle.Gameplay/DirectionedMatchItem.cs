using System;
using UnityEngine;

namespace PlatformPuzzle.Gameplay
{
    [Serializable]
    public class DirectionedMatchItem
    {
        [field: SerializeField]
        public Direction Direction { get; private set; }

        [field: SerializeField]
        public MatchItemMB MatchItem { get; private set; }
    }
}

