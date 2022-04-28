using UnityEngine;

namespace PlatformPuzzle.Gameplay.Tests.Editor
{
    internal static class GeneralUtil
    {
        public static T CreateGameObjectWithComponent<T>() where T : Component
        {
            GameObject gameObject = new GameObject();
            T component = gameObject.AddComponent<T>();

            return component;
        }
    }
}


