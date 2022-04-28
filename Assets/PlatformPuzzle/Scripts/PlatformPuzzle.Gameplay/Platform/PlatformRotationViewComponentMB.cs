using DG.Tweening;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using UnityEngine.Events;

namespace PlatformPuzzle.Gameplay
{
    internal class PlatformRotationViewComponentMB : MonoBehaviour, IPlatformComponent
    {
        [SerializeField]
        private BoolReference _canRotate;

        [SerializeField]
        private FloatReference _rotationAngles;

        [SerializeField]
        private RotateMode _rotateMode;

        [SerializeField]
        private Ease _rotationEase;

        [SerializeField]
        private FloatReference _rotationDuration;

        [Header("Events")]
        [SerializeField]
        private UnityEvent _onRotationStarted;

        [SerializeField]
        private UnityEvent _onRotationEnded;

        private bool _isRotating;
        private PlatformMB _platform;

        public void Setup(PlatformMB platform)
        {
            _platform = platform;
            _platform.RotatedRight += OnRotatedRight;
            _platform.RotatedLeft += OnRotatedLeft;
        }

        private void OnRotatedRight()
        {
            Vector3 rotationDelta = new Vector3(0, _rotationAngles, 0);
            Rotate(rotationDelta);
        }

        private void OnRotatedLeft()
        {
            Vector3 rotationDelta = new Vector3(0, -_rotationAngles, 0);
            Rotate(rotationDelta);
        }

        private void Rotate(Vector3 rotationDelta)
        {
            if (!CheckCanRotate())
            {
                return;
            }

            if (_isRotating)
            {
                transform.DOKill(true);
            }

            transform
                .DORotate(rotationDelta, _rotationDuration, _rotateMode)
                .SetEase(_rotationEase)
                .SetRelative()
                .OnStart(() =>
                {
                    _onRotationStarted?.Invoke();
                    _isRotating = true;
                })
                .OnComplete(() =>
                {

                    _onRotationEnded?.Invoke();
                    _isRotating = false;
                });
        }

        private bool CheckCanRotate()
        {
            bool result = _canRotate.Value;

            return result;
        }
    }
}

