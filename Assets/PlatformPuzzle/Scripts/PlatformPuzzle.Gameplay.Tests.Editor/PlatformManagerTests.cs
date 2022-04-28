using FluentAssertions;
using NUnit.Framework;

namespace PlatformPuzzle.Gameplay.Tests.Editor
{
    [TestFixture]
    internal class PlatformManagerTests
    {
        [TestCase(Direction.North, Direction.NorthEast)]
        [TestCase(Direction.NorthEast, Direction.SouthEast)]
        [TestCase(Direction.SouthEast, Direction.South)]
        [TestCase(Direction.South, Direction.SouthWest)]
        [TestCase(Direction.SouthWest, Direction.NorthWest)]
        [TestCase(Direction.NorthWest, Direction.North)]
        public void GetDirectionWithDelta_ToRight_ShouldBeExpected(
                Direction input, Direction expected)
        {
            Direction actual = PlatformManager.GetDirectionWithDelta(input, +1);

            actual.Should().Be(expected);
        }

        [TestCase(Direction.North, Direction.NorthWest)]
        [TestCase(Direction.NorthWest, Direction.SouthWest)]
        [TestCase(Direction.SouthWest, Direction.South)]
        [TestCase(Direction.South, Direction.SouthEast)]
        [TestCase(Direction.SouthEast, Direction.NorthEast)]
        [TestCase(Direction.NorthEast, Direction.North)]
        public void GetDirectionWithDelta_ToLeft_ShouldBeExpected(
                Direction input, Direction expected)
        {
            Direction actual = PlatformManager.GetDirectionWithDelta(input, -1);

            actual.Should().Be(expected);
        }

        [TestCase(Direction.North, Direction.South)]
        [TestCase(Direction.South, Direction.North)]
        [TestCase(Direction.NorthEast, Direction.SouthWest)]
        [TestCase(Direction.SouthWest, Direction.NorthEast)]
        [TestCase(Direction.SouthEast, Direction.NorthWest)]
        [TestCase(Direction.NorthWest, Direction.SouthEast)]
        public void GetOppositeDirection_WithTestCases_ShouldReturnExpected(
                Direction input, Direction expected)
        {
            Direction actual = PlatformManager.GetOppositeDirection(input);

            actual.Should().Be(expected);
        }
    }
}


