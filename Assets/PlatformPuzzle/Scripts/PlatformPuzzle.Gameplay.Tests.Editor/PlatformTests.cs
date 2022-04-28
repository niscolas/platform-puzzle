using FluentAssertions;
using NUnit.Framework;

namespace PlatformPuzzle.Gameplay.Tests.Editor
{
    [TestFixture]
    internal class PlatformTests
    {
        [TestCase(Direction.North, Direction.South)]
        [TestCase(Direction.South, Direction.North)]
        [TestCase(Direction.NorthEast, Direction.SouthWest)]
        [TestCase(Direction.SouthWest, Direction.NorthEast)]
        [TestCase(Direction.SouthEast, Direction.NorthWest)]
        [TestCase(Direction.NorthWest, Direction.SouthEast)]
        public void GetOppositeMatchItem_WithTestCases_ShouldReturnExpected(
                Direction direction1, Direction direction2)
        {
            PlatformMB platform = PlatformUtil.CreatePlatform();

            MatchItemMB matchItem1 = MatchItemUtil.CreateMatchItem();
            MatchItemMB matchItem2 = MatchItemUtil.CreateMatchItem();

            MatchItemWithDirection slot1 = new MatchItemWithDirection(direction1, matchItem1);
            MatchItemWithDirection slot2 = new MatchItemWithDirection(direction2, matchItem2);

            platform.MatchItemsWithDirection.Add(slot1);
            platform.MatchItemsWithDirection.Add(slot2);

            MatchItemMB oppositeMatchItem = platform.GetOppositeMatchItem(direction1);
            oppositeMatchItem.Should().Be(matchItem2);
        }

        [Test]
        public void RotateRight_WithAllDirections_AllShouldBeRotated()
        {
            SlotData slotNorth = new SlotData(Direction.North);
            SlotData slotNorthEast = new SlotData(Direction.NorthEast);
            SlotData slotSouthEast = new SlotData(Direction.SouthEast);
            SlotData slotSouth = new SlotData(Direction.South);
            SlotData slotSouthWest = new SlotData(Direction.SouthWest);
            SlotData slotNorthWest = new SlotData(Direction.NorthWest);

            PlatformMB platform = PlatformUtil.CreatePlatform();
            platform.Slots.Add(slotNorth);
            platform.Slots.Add(slotNorthEast);
            platform.Slots.Add(slotSouthEast);
            platform.Slots.Add(slotSouth);
            platform.Slots.Add(slotSouthWest);
            platform.Slots.Add(slotNorthWest);

            platform.RotateRight();

            platform.Slots[0].Direction.Should().Be(Direction.NorthEast);
            platform.Slots[1].Direction.Should().Be(Direction.SouthEast);
            platform.Slots[2].Direction.Should().Be(Direction.South);
            platform.Slots[3].Direction.Should().Be(Direction.SouthWest);
            platform.Slots[4].Direction.Should().Be(Direction.NorthWest);
            platform.Slots[5].Direction.Should().Be(Direction.North);
        }
    }
}

