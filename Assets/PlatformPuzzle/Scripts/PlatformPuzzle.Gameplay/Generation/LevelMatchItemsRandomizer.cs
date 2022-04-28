using UnityEngine;

namespace PlatformPuzzle.Gameplay
{
    internal class LevelMatchItemsRandomizer : MonoBehaviour, ILevelMatchItemsRandomizer
    {
        [SerializeField]
        private Vector2Int _rotationDeltaRange;

        public void Randomize(LevelData levelData)
        {
            foreach (PlatformMB platform in levelData.Platforms)
            {
                RandomizePlatform(platform);
            }
        }

        private void RandomizePlatform(PlatformMB platform)
        {
            int rotationDelta = GetNewRotationDelta();

            if (rotationDelta == 0)
            {
                return;
            }

            if (rotationDelta > 0)
            {
                RotateRightNTimes(platform, rotationDelta);
            }
            else
            {
                rotationDelta *= -1;
                RotateLeftNTimes(platform, rotationDelta);
            }
        }

        private int GetNewRotationDelta()
        {
            int result = Random.Range(
                    _rotationDeltaRange.x, _rotationDeltaRange.y + 1);

            return result;
        }

        private void RotateLeftNTimes(PlatformMB platform, int n)
        {
            for (int i = 0; i < n; i++)
            {
                platform.RotateLeft();
            }
        }

        private void RotateRightNTimes(PlatformMB platform, int n)
        {
            for (int i = 0; i < n; i++)
            {
                platform.RotateRight();
            }
        }
    }
}


