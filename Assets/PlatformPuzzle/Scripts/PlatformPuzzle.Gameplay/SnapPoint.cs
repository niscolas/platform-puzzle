using System;
using UnityEngine;

namespace PlatformPuzzle.Gameplay
{
    [Serializable]
    internal class SnapPoint
    {
        [field: SerializeField]
        public Direction Direction { get; private set; }

        [field: SerializeField]
        public PlatformMB Platform { get; private set; }

        [field: SerializeField]
        public Transform ReferencePoint { get; private set; }

        public bool CheckIsAvailable()
        {
            bool result = !Platform;

            return result;
        }

        public void SetDirection(Direction direction)
        {
            Direction = direction;
        }

        public void SetPlatform(PlatformMB platform)
        {
            Platform = platform;
        }
    }
}

