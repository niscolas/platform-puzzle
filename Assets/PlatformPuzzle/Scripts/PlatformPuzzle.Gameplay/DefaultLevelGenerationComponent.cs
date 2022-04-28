using System;
using System.Collections.Generic;
using System.Linq;
using UnityAtoms.BaseAtoms;
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

    internal class DefaultLevelGenerationComponent : MonoBehaviour, ILevelGenerator
    {
        [SerializeField]
        private IntReference _platformCount;

        [SerializeField]
        private FloatReference _maxPlatformDistance;

        [SerializeField]
        private GameObject _platformPrefab;

        [SerializeField]
        private List<AngleRangeDirection> _angleRangeDirections;

        private readonly List<PlatformMB> _platforms = new List<PlatformMB>();

        private Transform _cachedTransform;

        public void Setup()
        {
            _cachedTransform = transform;
        }

        public void Generate()
        {
            if (!CheckCanGenerate())
            {
                return;
            }


            int remainingPlatformCount = _platformCount.Value;
            GeneratePlatform(_cachedTransform.position, remainingPlatformCount);
        }

        public void GeneratePlatform(
                Vector3 position,
                int remainingPlatformCount,
                PlatformMB currentPivotPlatform = default)
        {
            GameObject platformGameObject = Instantiate(
                    _platformPrefab, position, Quaternion.identity);

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

            GeneratePlatform(
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
            Direction[] directionsArray = PlatformManager.DirectionsArray;

            for (int i = 0; i < directionsArray.Length; i++)
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

                Debug.Log($"direction between platform at {platform.Position} and {otherPlatform.Position} = {direction}");

                Direction oppositeDirection = PlatformManager.GetOppositeDirection(direction);

                Debug.Log($"oppositeDirection = {oppositeDirection}");

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

                    Debug.Log(angle + " " + direction);

                    return true;
                }
            }

            Debug.Log("Could not get direction for angle");

            return false;
        }

        private bool CheckIsBetween(float value, float min, float max)
        {
            bool result = (value - min) * (max - value) >= 0;

            return result;
        }

        private bool CheckCanGenerate()
        {
            bool result = _platformCount.Value >= 1;

            return result;
        }
    }
}

