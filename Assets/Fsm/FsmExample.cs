
using UnityEngine;

namespace FSM.Scripts
{
    public class FsmExample : MonoBehaviour
    {
        private Fsm _fsm;
        private void Start()
        {
            _fsm = new Fsm();

            _fsm.AddState(new FsmStateIdle(_fsm));
            _fsm.AddState(new FsmStateSearch(_fsm));
            _fsm.AddState(new FsmStateCollect(_fsm));

            _fsm.SetState<FsmStateIdle>();
        }

        private void Update()
        {
            _fsm.Update();
        }

    }
}
    
