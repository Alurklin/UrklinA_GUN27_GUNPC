
using UnityEngine;

namespace FSM.Scripts
{
    public class FsmExample : MonoBehaviour
    {
        private Animator _animator;
        private Fsm _fsm;

        private void Start()
        {
            _animator = GetComponent<Animator>();

            _fsm = new Fsm(_animator);

            _fsm = new Fsm(_animator);  // Передаем Animator в Fsm
            _fsm.AddState(new FsmStateIdle(_fsm, _animator));
            _fsm.AddState(new FsmStateSearch(_fsm, _animator));
            _fsm.AddState(new FsmStateCollect(_fsm, _animator));

            _fsm.SetState<FsmStateIdle>();
        }

        private void Update()
        {
            _fsm.Update();
        }

    }

}
    
