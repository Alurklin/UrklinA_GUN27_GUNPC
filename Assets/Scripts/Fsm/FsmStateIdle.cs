using UnityEngine;
using FSM.Scripts;

public class FsmStateIdle : FsmState
{
    private Animator _animator;

    private float _timer;
    private const float IdleDuration = 5f;

    public FsmStateIdle(Fsm fsm, Animator animator) : base(fsm) 
    {
        _animator = animator;  // Сохраняем Animator
    }

    public override void Enter()
    {
        _timer = 0f;

        Debug.Log("Entering Idle State: Playing Idle Animation");
        _animator.SetTrigger("Idle");
    }

    public override void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= IdleDuration)
        {
            Fsm.SetState<FsmStateSearch>();
        }
    }
}

