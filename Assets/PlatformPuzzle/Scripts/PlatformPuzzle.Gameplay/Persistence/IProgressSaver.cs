namespace PlatformPuzzle.Gameplay
{
    internal interface IProgressSaver
    {

        void IncrementProgress();
        void Save();
        int Load();
    }
}


