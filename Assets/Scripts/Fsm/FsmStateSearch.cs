using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM.Scripts;
using System.Linq;

public class FsmStateSearch : FsmState
{
    private Animator _animator;

    private int _currentTargetIndex;
    private Transform[] _targets;
    private Transform _agent;
    private float _speed = 1f;

    public FsmStateSearch(Fsm fsm, Animator animator) : base(fsm)
    {
        _animator = animator;  // Сохраняем Animator
        // Initialize targets and agent in your game context
        _targets = GameObject.FindGameObjectsWithTag("Target").Select(obj => obj.transform).ToArray();
        _agent = GameObject.FindGameObjectWithTag("Agent").transform;
    }

    public override void Enter()
    {
        _currentTargetIndex = 0;
        Debug.Log("Entering Search State: Playing Walk Animation");
        _animator.SetTrigger("Search");
    }

    public override void Update()
    {
        if (_targets.Length == 0) return;

        var target = _targets[_currentTargetIndex];

        // Перемещаем агента к цели
        _agent.position = Vector3.MoveTowards(_agent.position, target.position, _speed * Time.deltaTime);

    }

}
