using System.Collections.Generic;
using System.Linq;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using UnityEngine.Events;

namespace PlatformPuzzle.Gameplay
{
    internal class DefaultLevelGenerationComponent : MonoBehaviour, ILevelGenerator
    {
        [SerializeField]
        private FloatReference _maxPlatformDistance;

        [SerializeField]
        private GameObject _platformPrefab;

        [SerializeField]
        private List<AngleRangeDirection> _angleRangeDirections;

        [SerializeField]
        private Transform _platformsParent;

        [Header("Events")]
        [SerializeField]
        private UnityEvent _onPlatformCreated;

        private readonly List<PlatformMB> _platforms = new List<PlatformMB>();

        private Transform _cachedTransform;

        public void Setup()
        {
            _cachedTransform = transform;
        }

        public LevelData Generate(LevelGeneratorData data)
        {
            if (!CheckCanGenerate(data))
            {
                return default;
            }

            GeneratePlatformRec(_cachedTransform.position, data.DifficultyLevel * 2);
            foreach(PlatformMB platform in _platforms)
            {
                platform.Setup();
            }
            LevelData levelData = new LevelData(_platforms);

            return levelData;
        }

        public void GeneratePlatformRec(
                Vector3 position,
                int remainingPlatformCount,
                PlatformMB currentPivotPlatform = default)
        {
            GameObject platformGameObject = Instantiate(
                    _platformPrefab, position, Quaternion.identity, _platformsParent);

            _onPlatformCreated?.Invoke();

            PlatformMB platform = platformGameObject.GetComponentInChildren<PlatformMB>();
            AttachExistingPlatforms(platform);
            _platforms.Add(platform);

            remainingPlatformCount--;

            if (remainingPlatformCount == 0 || !platform.CheckHasAvailableSnapPoints())
            {
                return;
            }

            if (!currentPivotPlatform)
            {
                currentPivotPlatform = platform;
            }
            else
            {
                if (!currentPivotPlatform.CheckHasAvailableSnapPoints())
                {
                    currentPivotPlatform = GetRandomAttachedPlatform(currentPivotPlatform);
                }
            }

            Vector3 randomSpawnPosition = GetNewSpawnPointFromPlatform(currentPivotPlatform);

            GeneratePlatformRec(
                    randomSpawnPosition,
                    remainingPlatformCount,
                    currentPivotPlatform);
        }

        private PlatformMB GetRandomAttachedPlatform(PlatformMB platform)
        {
            PlatformMB[] possiblePivotPlatforms =
                platform.GetAttachedPlatforms().ToArray();

            int randomPivotPlatformIndex = UnityEngine.Random.Range(0, possiblePivotPlatforms.Length);
            PlatformMB randomPivotPlatform = possiblePivotPlatforms[randomPivotPlatformIndex];

            return randomPivotPlatform;
        }

        private Vector3 GetNewSpawnPointFromPlatform(PlatformMB platform)
        {
            List<Direction> directionsArray = PlatformManager.DirectionsArray;

            for (int i = 0; i < directionsArray.Count; i++)
            {
                SnapPoint snapPoint = platform
                    .GetSlotForDirection(directionsArray[i]).SnapPoint;
                bool hasPlatformForDirection = snapPoint.Platform;

                if (!hasPlatformForDirection)
                {
                    return snapPoint.ReferencePoint.position;
                }
            }

            return Vector3.zero;
        }

        private void AttachExistingPlatforms(PlatformMB platform)
        {
            foreach (PlatformMB otherPlatform in _platforms)
            {
                float distance = Vector3.Distance(
                        platform.Position, otherPlatform.Position);
                if (distance > _maxPlatformDistance.Value)
                {
                    continue;
                }

                float angleBetween = GetAngleBetweenPlatforms(platform, otherPlatform);

                if (!TryGetDirectionForAngle(angleBetween, out Direction direction))
                {
                    continue;
                }

                Direction oppositeDirection = PlatformManager.GetOppositeDirection(direction);

                SlotData slot = platform.GetSlotForDirection(direction);
                SlotData otherPlatformSlot = otherPlatform.GetSlotForDirection(oppositeDirection);

                if (slot.SnapPoint.Platform == otherPlatform &&
                        otherPlatformSlot.SnapPoint.Platform == platform)
                {
                    continue;
                }

                slot.SnapPoint.SetPlatform(otherPlatform);
                otherPlatformSlot.SnapPoint.SetPlatform(platform);
            }
        }

        private float GetAngleBetweenPlatforms(PlatformMB platform, PlatformMB otherPlatform)
        {
            Vector3 platformPosition = platform.Position;
            Vector3 otherPlatformPosition = otherPlatform.Position;

            float angle = Vector3.Angle(platform.transform.forward, otherPlatformPosition - platformPosition);

            if (platformPosition.x > otherPlatformPosition.x)
            {
                angle *= -1;
            }

            return angle;
        }

        private bool TryGetDirectionForAngle(float angle, out Direction direction)
        {
            direction = default;

            for (int i = 0; i < _angleRangeDirections.Count; i++)
            {

                bool isInRange = CheckIsBetween(
                        angle,
                        _angleRangeDirections[i].AngleRange.x,
                        _angleRangeDirections[i].AngleRange.y); ;

                if (isInRange)
                {
                    direction = _angleRangeDirections[i].Direction;

                    return true;
                }
            }

            return false;
        }

        private bool CheckIsBetween(float value, float min, float max)
        {
            bool result = (value - min) * (max - value) >= 0;

            return result;
        }

        private bool CheckCanGenerate(LevelGeneratorData data)
        {
            bool result = data.DifficultyLevel >= 1;

            return result;
        }
    }
}

