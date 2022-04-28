namespace PlatformPuzzle.Gameplay
{
    internal class GameplaySystemMB : MiddleMast.GameplayFramework.System
    {
        public override void Setup()
        {
            ILevelGenerator levelGenerator = GetComponentInChildren<ILevelGenerator>();
            levelGenerator.Setup();
            levelGenerator.Generate();
        }
    }
}

