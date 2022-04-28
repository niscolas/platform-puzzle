using UnityAtoms.BaseAtoms;
using UnityEngine;

namespace PlatformPuzzle.Gameplay
{
    internal class ProgressSaverComponentMB : MonoBehaviour, IProgressSaver
    {
        [SerializeField]
        private IntReference _currentLevelNumber;

        private const string CurrentLevelKey = "CurrentLevel";

        public void Save()
        {
            PlayerPrefs.SetInt(CurrentLevelKey, _currentLevelNumber.Value);
        }

        public int Load()
        {
            int currentLevel = PlayerPrefs.GetInt(CurrentLevelKey, _currentLevelNumber.Value);

            return currentLevel;
        }
    }
}


