using UnityEngine;

namespace PlatformPuzzle.Gameplay
{
    internal class AngleTestMB : MonoBehaviour
    {
        [SerializeField]
        private Transform _point1;

        [SerializeField]
        private Transform _point2;

        [ContextMenu(nameof(PrintAngle))]
        public void PrintAngle()
        {
            Vector3 from = _point1.forward;
            Vector3 to = _point2.position - _point1.position;
            Debug.Log(Vector3.Angle(from, to));
        }
    }
}


