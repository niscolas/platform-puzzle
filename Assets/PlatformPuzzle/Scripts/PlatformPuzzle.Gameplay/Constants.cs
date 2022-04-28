using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("PlatformPuzzle.Gameplay" + PlatformPuzzle.Constants.EditorTestAssemblySuffix)]

namespace PlatformPuzzle.Gameplay
{

    public static class Constants
    {
        public const string CreateAssetMenuPrefix =
            PlatformPuzzle.Constants.CreateAssetMenuPrefix;

        public const int CreateAssetMenuOrder =
            PlatformPuzzle.Constants.CreateAssetMenuOrder;
    }
}
