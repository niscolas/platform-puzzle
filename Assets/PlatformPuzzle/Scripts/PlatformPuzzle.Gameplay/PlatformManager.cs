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

        public static Direction GetOppositeDirection(Direction direction)
        {
            Direction result = OppositeDirectionMap[direction];

            return result;
        }
    }
}

