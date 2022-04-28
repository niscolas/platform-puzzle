using System;
using System.Collections.Generic;
using System.Linq;
using MiddleMast.GameplayFramework;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using UnityEngine.Serialization;

namespace PlatformPuzzle.Gameplay
{
    internal class PlatformMB : Entity
    {
        [SerializeField]
        private BoolReference _canRotate;

        [field: SerializeField]
        public List<SlotData> Slots { get; private set; }
            = new List<SlotData>();

        [field: FormerlySerializedAs("MatchItems")]
        [field: SerializeField]
        public List<MatchItemWithDirection> MatchItemsWithDirection { get; private set; }
        = new List<MatchItemWithDirection>();

        public event Action RotatedRight;
        public event Action RotatedLeft;

        public Vector3 Position => transform.position;

        public override void Setup()
        {
            SetupComponents();
        }

        public void SolveMatches()
        {
            IEnumerable<PlatformMB> attachedPlatforms = GetAttachedPlatforms();

            foreach (PlatformMB otherPlatform in attachedPlatforms)
            {
                SolveMatchWith(otherPlatform);
            }
        }

        public void SolveMatchWith(PlatformMB otherPlatform)
        {
            Direction direction = GetDirectionOfAttachedPlatform(otherPlatform);
            MatchItemMB matchItem = GetMatchItemOfDirection(direction);

            if (!matchItem.IsEnabled)
            {
                return;
            }

            Direction attachedDirection = otherPlatform.GetDirectionOfAttachedPlatform(this);
            MatchItemMB otherMatchItem = otherPlatform.GetMatchItemOfDirection(attachedDirection);
            if (!otherMatchItem.IsEnabled)
            {
                return;
            }

            bool matchResult = matchItem.CompareWith(otherMatchItem);

            if (matchResult)
            {
                matchItem.OnMatch();
                otherMatchItem.OnMatch();
            }
        }

        public IEnumerable<MatchItemMB> GetEnabledMatchItems()
        {
            IEnumerable<MatchItemMB> result = MatchItemsWithDirection
                .Select(x => x.MatchItem)
                .Where(x => x.IsEnabled);

            return result;
        }

        public IEnumerable<MatchItemMB> GetMatchItems()
        {
            IEnumerable<MatchItemMB> result = MatchItemsWithDirection.Select(x => x.MatchItem);

            return result;
        }

        public void TriggerMatchAtDirection(Direction direction)
        {
            MatchItemMB matchItem = GetMatchItemOfDirection(direction);
            matchItem.OnMatch();
        }

        private MatchItemMB GetMatchItemOfDirection(Direction direction)
        {
            MatchItemMB result = MatchItemsWithDirection
                .Where(x => x.Direction == direction)
                .Select(x => x.MatchItem)
                .FirstOrDefault();

            return result;
        }

        public int GetEnabledMatchItemsCount()
        {
            int result = MatchItemsWithDirection.Count(x => x.MatchItem.IsEnabled);

            return result;
        }

        public bool CheckHasEnabledMatchItem()
        {
            bool result = MatchItemsWithDirection.Any(x => x.MatchItem.IsEnabled);

            return result;
        }

        public SlotData GetSlotForDirection(Direction direction)
        {
            SlotData result = Slots.FirstOrDefault(x => x.Direction == direction);

            return result;
        }

        public MatchItemMB GetOppositeMatchItem(Direction direction)
        {
            Direction oppositeDirection = PlatformManager.GetOppositeDirection(direction);
            MatchItemMB oppositeMatchItem = MatchItemsWithDirection.FirstOrDefault(
                    x => x.Direction == oppositeDirection).MatchItem;

            return oppositeMatchItem;
        }

        public bool CheckHasAvailableSnapPoints()
        {
            bool result = Slots.Any(x => x.SnapPoint.CheckIsAvailable());

            return result;
        }

        public IEnumerable<SnapPoint> GetAvailableSnapPoints()
        {
            IEnumerable<SnapPoint> result = Slots
                .Select(x => x.SnapPoint)
                .Where(x => x.CheckIsAvailable());

            return result;
        }

        public Direction GetDirectionOfAttachedPlatform(PlatformMB otherPlatform)
        {
            Direction result = Slots
                .Where(x => x.SnapPoint.Platform == otherPlatform)
                .Select(x => x.Direction)
                .FirstOrDefault();

            return result;
        }

        public IEnumerable<PlatformMB> GetAttachedPlatforms()
        {
            IEnumerable<PlatformMB> result = Slots
                .Where(x => x.SnapPoint.Platform)
                .Select(x => x.SnapPoint.Platform);

            return result;
        }

        public void RotateLeft()
        {
            Rotate(+1);
            RotatedLeft?.Invoke();
        }

        public void RotateRight()
        {
            Rotate(-1);
            RotatedRight?.Invoke();
        }

        private void SetupComponents()
        {
            IEnumerable<IPlatformComponent> components =
                GetComponentsInChildren<IPlatformComponent>();

            foreach (IPlatformComponent component in components)
            {
                component.Setup(this);
            }
        }

        private void Rotate(int delta)
        {
            if (!CheckCanRotate())
            {
                return;
            }

            foreach (SlotData slot in Slots)
            {
                Direction newDirection = PlatformManager.GetDirectionWithDelta(
                        slot.Direction,
                        delta);
                slot.SetDirection(newDirection);
            }
        }

        private bool CheckCanRotate()
        {
            bool result = _canRotate.Value && Slots.Count > 1;

            return result;
        }
    }
}
