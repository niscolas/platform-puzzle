using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MiddleMast.GameplayFramework;
using UnityEngine;

namespace PlatformPuzzle.Gameplay
{
    internal class PlatformMB : Entity
    {
        [field: SerializeField]
        public List<SnapPoint> SnapPoints { get; private set; }
            = new List<SnapPoint>();

        [field: SerializeField]
        public List<DirectionedMatchItem> MatchItems { get; private set; }
            = new List<DirectionedMatchItem>();

        [field: SerializeField]
        public List<PlatformMB> NotSnappedPlatforms { get; private set; }
            = new List<PlatformMB>();

        private PlatformManager _manager;

        public void Setup(
            PlatformManager manager,
            IEnumerable<PlatformMB> notSnappedPlatforms)
        {
            _manager = manager;
            NotSnappedPlatforms = notSnappedPlatforms.ToList();
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
            MatchItemMB oppositeMatchItem = MatchItems.FirstOrDefault(
                    x => x.Direction == oppositeDirection).MatchItem;

            return oppositeMatchItem;
        }

        public bool CheckHasAvailableSnapPoints()
        {
            bool result = SnapPoints.Any(x => x.CheckIsAvailable());

            return result;
        }

        public IEnumerable<SnapPoint> GetAvailableSnapPoints()
        {
            IEnumerable<SnapPoint> result = SnapPoints.Where(x => x.CheckIsAvailable());

            return result;
        }

        public void RotateLeft()
        {
            if (!CheckCanRotate())
            {
                return;
            }

            int lastIndex = SnapPoints.Count - 1;

            Direction currentDirection = SnapPoints[lastIndex].Direction;
            SnapPoints[lastIndex].SetDirection(SnapPoints[0].Direction);

            for (int i = lastIndex - 1; i >= 0; i++)
            {
                Direction nextDirection = SnapPoints[i].Direction;
                SnapPoints[i].SetDirection(currentDirection);
                currentDirection = nextDirection;
            }
        }

        public void RotateRight()
        {
            if (!CheckCanRotate())
            {
                return;
            }

            int lastIndex = SnapPoints.Count - 1;

            Direction currentDirection = SnapPoints[0].Direction;
            SnapPoints[0].SetDirection(SnapPoints[lastIndex].Direction);

            for (int i = 1; i < SnapPoints.Count; i++)
            {
                Direction nextDirection = SnapPoints[i].Direction;
                SnapPoints[i].SetDirection(currentDirection);
                currentDirection = nextDirection;
            }
        }

        private bool CheckCanRotate()
        {
            bool result = SnapPoints.Count > 1;

            return result;
        }
    }
}
