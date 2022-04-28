namespace PlatformPuzzle.Gameplay.Tests.Editor
{
    internal static class PlatformUtil
    {
        public static PlatformMB CreatePlatform()
        {
            PlatformMB platform = GeneralUtil.CreateGameObjectWithComponent<PlatformMB>();

            return platform;
        }
    }
}


