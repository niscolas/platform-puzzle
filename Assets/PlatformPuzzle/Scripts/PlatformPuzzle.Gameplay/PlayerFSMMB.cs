using FSM;
using MiddleMast.GameplayFramework;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using UnityEngine.Events;

namespace PlatformPuzzle.Gameplay
{
    internal class PlayerFSMMB : Entity
    {
        [SerializeField]
        private BoolReference _isInputEnabled;

        [Header("Events")]
        [SerializeField]
        private UnityEvent _onInputAllowed;

        [SerializeField]
        private UnityEvent _onInputBlocked;

        private StateMachine _fsm;

        private const string InputAllowedStateName = nameof(InputAllowedStateName);
        private const string InputBlockedStateName = nameof(InputBlockedStateName);
        private const string AllowInputTrigger = nameof(AllowInputTrigger);
        private const string BlockInputTrigger = nameof(BlockInputTrigger);

        public override void Setup()
        {
            _fsm = new StateMachine();

            _fsm.AddState(InputAllowedStateName, new State(
                onEnter: _ =>
                {
                    _isInputEnabled.Value = true;
                    _onInputAllowed?.Invoke();
                }));

            _fsm.AddState(InputBlockedStateName, new State(
                onEnter: _ =>
                {
                    _isInputEnabled.Value = false;
                    _onInputBlocked?.Invoke();
                }));

            _fsm.AddTriggerTransition(AllowInputTrigger,
                new Transition(InputBlockedStateName, InputAllowedStateName));

            _fsm.AddTriggerTransition(BlockInputTrigger,
                new Transition(InputAllowedStateName, InputBlockedStateName));

            _fsm.SetStartState(InputAllowedStateName);

            _fsm.Init();
        }

        public void AllowInput()
        {
            _fsm.Trigger(AllowInputTrigger);
        }

        public void BlockInput()
        {
            _fsm.Trigger(BlockInputTrigger);
        }
    }
}


