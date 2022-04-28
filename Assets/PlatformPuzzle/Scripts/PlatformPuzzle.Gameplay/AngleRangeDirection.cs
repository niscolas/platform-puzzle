using System;
using UnityEngine;

namespace PlatformPuzzle.Gameplay
{
    [Serializable]
    public class AngleRangeDirection
    {
        [field: SerializeField]
        public Vector2 AngleRange { get; private set; }

        [field: SerializeField]
        public Direction Direction { get; private set; }
    }
}


