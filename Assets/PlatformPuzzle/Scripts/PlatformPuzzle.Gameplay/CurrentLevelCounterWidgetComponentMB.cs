using TMPro;
using UnityAtoms;
using UnityAtoms.BaseAtoms;
using UnityEngine;

namespace PlatformPuzzle.Gameplay
{
    internal class CurrentLevelCounterWidgetComponentMB : MonoBehaviour, IAtomListener<int>
    {
        [SerializeField]
        private TMP_Text _text;

        [SerializeField]
        private IntEventReference _currentLevelNumberChangedEvent;

        [SerializeField]
        private bool _replayEventBuffer;

        private void Awake()
        {
            _currentLevelNumberChangedEvent.GetEvent<IntEvent>().RegisterListener(this, _replayEventBuffer);
        }

        private void OnDestroy()
        {
            _currentLevelNumberChangedEvent.GetEvent<IntEvent>().UnregisterListener(this);
        }

        public void OnEventRaised(int item)
        {
            _text.SetText($"Level: {item}");
        }
    }
}


