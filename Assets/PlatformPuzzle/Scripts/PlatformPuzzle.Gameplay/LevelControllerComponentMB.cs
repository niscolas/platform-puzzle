using UnityEngine;
using UnityEngine.SceneManagement;

namespace PlatformPuzzle.Gameplay
{

    internal class LevelControllerComponentMB : MonoBehaviour
    {
        public void ReloadLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void LoadNextLevel()
        {
            ReloadLevel();
        }
    }
}

