using DG.Tweening;
using UnityAtoms.BaseAtoms;
using UnityEngine;

namespace PlatformPuzzle.Gameplay
{
    internal class MatchItemDisableViewComponentMB : MonoBehaviour, IMatchItemComponent
    {
        [SerializeField]
        private Vector3Reference _moveOffset;

        [SerializeField]
        private FloatReference _moveDuration;

        [SerializeField]
        private Ease _moveEase;

        private MatchItemMB _matchItem;

        public void Setup(MatchItemMB matchItem)
        {
            _matchItem = matchItem;
            _matchItem.Matched += OnMatched;
        }

        private void OnMatched()
        {
            transform.SetParent(null);

            transform
                .DOMove(_moveOffset.Value, _moveDuration.Value)
                .SetEase(_moveEase)
                .SetRelative()
                .OnComplete(() =>
                {
                    gameObject.SetActive(false);
                    transform.DOKill();
                });
        }
    }
}


