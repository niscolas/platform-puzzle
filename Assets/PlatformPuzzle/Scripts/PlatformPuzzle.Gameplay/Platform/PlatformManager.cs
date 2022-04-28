using System;
using System.Collections.Generic;
using MiddleMast.GameplayFramework;

namespace PlatformPuzzle.Gameplay
{
    // TODO: rename PlatformManagerMB
    internal class PlatformManager : Manager
    {
        private static readonly Dictionary<Direction, Direction> OppositeDirectionMap =
            new Dictionary<Direction, Direction>
            {
                {Direction.North, Direction.South},
                {Direction.South, Direction.North},
                {Direction.NorthEast, Direction.SouthWest},
                {Direction.SouthWest, Direction.NorthEast},
                {Direction.NorthWest, Direction.SouthEast},
                {Direction.SouthEast, Direction.NorthWest},
            };

        public static readonly List<Direction> DirectionsArray =
            new List<Direction>
        {
            Direction.North,
            Direction.NorthEast,
            Direction.SouthEast,
            Direction.South,
            Direction.SouthWest,
            Direction.NorthWest,
        };

        public static Direction GetOppositeDirection(Direction direction)
        {
            Direction result = OppositeDirectionMap[direction];

            return result;
        }

        public static Direction GetDirectionAtIndex(int index)
        {
            Direction result = DirectionsArray[index];

            return result;
        }

        public static Direction GetDirectionWithDelta(Direction direction, int delta)
        {
            int directionCount = DirectionsArray.Count;
            int directionIndex = DirectionsArray.IndexOf(direction);
            int newDirectionIndex = directionIndex + delta;

            if (newDirectionIndex < 0)
            {
                newDirectionIndex += directionCount;
            }
            else if (newDirectionIndex >= directionCount)
            {
                newDirectionIndex -= directionCount;
            }

            Direction result = DirectionsArray[newDirectionIndex];

            return result;
        }
    }
}

