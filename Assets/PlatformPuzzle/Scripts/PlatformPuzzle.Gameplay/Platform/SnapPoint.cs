using System;
using UnityEngine;

namespace PlatformPuzzle.Gameplay
{
    [Serializable]
    internal class SnapPoint
    {
        [field: SerializeField]
        public PlatformMB Platform { get; private set; }

        [field: SerializeField]
        public Transform ReferencePoint { get; private set; }

        public bool CheckIsAvailable()
        {
            bool result = !Platform;

            return result;
        }

        public void SetPlatform(PlatformMB platform)
        {
            Platform = platform;
        }
    }
}

