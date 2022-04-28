namespace PlatformPuzzle.Gameplay
{
    internal interface ILevelGenerator
    {
        void Setup();
        LevelData Generate(LevelGeneratorData data);
    }
}

