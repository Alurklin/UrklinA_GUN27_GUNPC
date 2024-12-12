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
        _animator = animator;  // ��������� Animator
        // Initialize targets and agent in your game context
        _targets = GameObject.FindGameObjectsWithTag("Target").Select(obj => obj.transform).ToArray();
        _agent = GameObject.FindGameObjectWithTag("Agent").transform;
    }

    public override void Enter()
    {
        _currentTargetIndex = 0;
        Debug.Log("Entering Search State: Playing Walk Animation");
        _animator.SetTrigger("Search");

        // ������������� �� ������� ��������
        var fsmExample = GameObject.FindGameObjectWithTag("Agent").GetComponent<FsmExample>();
        fsmExample.OnCollisionDetected += HandleCollision;  // �������� �� �������
    }

    public override void Update()
    {
        if (_targets.Length == 0) return;

        var target = _targets[_currentTargetIndex];

        // ���������� ������ � ����
        _agent.position = Vector3.MoveTowards(_agent.position, target.position, _speed * Time.deltaTime);

        if (Vector3.Distance(_agent.position, target.position) < 0.1f)
        {
            Debug.Log($"Target {_currentTargetIndex} reached.");

            // ������� � ��������� ����
            _currentTargetIndex++;

            // ���� ��� ���� ��������� ����, ������������ � ������ ��� ����������
            if (_currentTargetIndex >= _targets.Length)
            {
                Debug.Log("All targets reached.");
                _currentTargetIndex = 0;  // ���� ������, ����� �������� ������� ����� � ������ ����
                // ����� ������������� �� ������ ���������, ���� �����:
                Fsm.SetState<FsmStateIdle>();
            }
        }

    }

    public override void Exit()
    {
        // ������������ �� �������, ����� ������� �� ���������
        var fsmExample = GameObject.FindGameObjectWithTag("Agent").GetComponent<FsmExample>();
        fsmExample.OnCollisionDetected -= HandleCollision;
    }

    private void HandleCollision(GameObject target)
    {
        Debug.Log("Handling collision with target: " + target.name);

        GameObject.Destroy(target);  // ���������� ������

    }

}
