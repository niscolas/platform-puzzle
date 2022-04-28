using System.Collections.Generic;

namespace PlatformPuzzle.Gameplay
{
    internal class LevelData
    {
        public List<PlatformMB> Platforms;

        public LevelData(List<PlatformMB> platforms)
        {
            Platforms = platforms;
        }
    }
}

