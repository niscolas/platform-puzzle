namespace PlatformPuzzle.Gameplay.Tests.Editor
{
    internal static class MatchItemUtil
    {
        public static MatchItemMB CreateMatchItem()
        {
            MatchItemMB matchItem = GeneralUtil.CreateGameObjectWithComponent<MatchItemMB>();

            return matchItem;
        }
    }
}


