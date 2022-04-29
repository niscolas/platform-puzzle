using UnityAtoms.BaseAtoms;
using UnityEngine;

namespace PlatformPuzzle.Gameplay
{
    internal class ProgressSaverComponentMB : MonoBehaviour, IProgressSaver
    {
        [SerializeField]
        private IntReference _currentLevelNumber;

        private const string CurrentLevelKey = "CurrentLevel";

        public void DecrementProgress()
        {
            _currentLevelNumber.Value -= 1;
            Save();
        }

        public void IncrementProgress()
        {
            _currentLevelNumber.Value += 1;
            Save();
        }

        public void Save()
        {
            PlayerPrefs.SetInt(CurrentLevelKey, _currentLevelNumber.Value);
        }

        public int Load()
        {
            int currentLevel = PlayerPrefs.GetInt(CurrentLevelKey, _currentLevelNumber.Value);
            _currentLevelNumber.Value = currentLevel;

            return currentLevel;
        }
    }
}


