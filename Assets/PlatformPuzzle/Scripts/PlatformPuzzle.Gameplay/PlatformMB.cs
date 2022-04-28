using System.Collections.Generic;
using System.Linq;
using MiddleMast.GameplayFramework;
using UnityEngine;

namespace PlatformPuzzle.Gameplay
{
    internal class PlatformMB : Entity
    {
        [field: SerializeField]
        public List<SlotData> Slots { get; private set; }
            = new List<SlotData>();

        [field: SerializeField]
        public List<PlatformMB> NotSnappedPlatforms { get; private set; }
            = new List<PlatformMB>();

        public Vector3 Position => transform.position;

        public void Setup(
            IEnumerable<PlatformMB> notSnappedPlatforms)
        {
            NotSnappedPlatforms = notSnappedPlatforms.ToList();
        }

        public SlotData GetSlotForDirection(Direction direction)
        {
            SlotData result = Slots.FirstOrDefault(x => x.Direction == direction);

            return result;
        }

        public bool CompareWithMatchItemOfDirection(
                MatchItemMB matchItem, Direction matchItemDirection)
        {
            MatchItemMB oppositeMatchItem = GetOppositeMatchItem(matchItemDirection);
            bool result = matchItem.CompareWith(oppositeMatchItem);

            return result;
        }

        public MatchItemMB GetOppositeMatchItem(Direction direction)
        {
            Direction oppositeDirection = PlatformManager.GetOppositeDirection(direction);
            MatchItemMB oppositeMatchItem = Slots.FirstOrDefault(
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

        public IEnumerable<PlatformMB> GetAttachedPlatforms()
        {
            IEnumerable<PlatformMB> result = Slots
                .Where(x => x.SnapPoint.Platform)
                .Select(x => x.SnapPoint.Platform);

            return result;
        }

        public void RotateLeft()
        {
            Rotate(-1);
        }

        public void RotateRight()
        {
            Rotate(+1);
        }

        public void Rotate(int delta)
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
            bool result = Slots.Count > 1;

            return result;
        }
    }
}
