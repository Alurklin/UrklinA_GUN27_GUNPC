using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM.Scripts;

public class FsmStateCollect : FsmState
{
    private Animator _animator;

    private GameObject _currentTarget;
    private float _timer;
    private const float CollectDuration = 1f;

    public FsmStateCollect(Fsm fsm, Animator animator) : base(fsm) 
    {
        _animator = animator;  // Сохраняем Animator
    }

    public override void Enter()
    {
        _timer = 0f;
        Debug.Log("Entering Collect State: Playing Collect Animation");

        _animator.SetTrigger("Collect");
    }

    public override void Update()
    {
        if (_currentTarget == null) return;

        _timer += Time.deltaTime;

        if (_timer >= CollectDuration)
        {
            if (_currentTarget != null)
            {
                Debug.Log("Collecting and Removing Object");
                GameObject.Destroy(_currentTarget);
                _currentTarget = null;  // Очищаем ссылку на цель
            }
            Fsm.SetState<FsmStateIdle>();
        }
    }

    public void SetTarget(GameObject target)
    {
        _currentTarget = target;
    }
}
