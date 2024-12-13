using UnityEngine;
using FSM.Scripts;
using System.Linq;
using UnityEngine.AI;

public class FsmStateSearch : FsmState
{
    private Animator _animator;

    private int _currentTargetIndex;
    private Transform[] _targets;
    private NavMeshAgent _agent;

    public FsmStateSearch(Fsm fsm, Animator animator) : base(fsm)
    {
        _animator = animator;  // ��������� Animator
        _agent = GameObject.FindGameObjectWithTag("Agent").GetComponent<NavMeshAgent>();  // �������� ��������� NavMeshAgent
    }

    public override void Enter()
    {
        // Initialize targets and agent in your game context
        _targets = GameObject.FindGameObjectsWithTag("Target").Select(obj => obj.transform).ToArray();
        _currentTargetIndex = 0;

        if (_targets.Length != 0)
        {
            Debug.Log("Entering Search State: Playing Walk Animation");
            _animator.SetTrigger("Search");
        }
        else 
        {
            Debug.Log("No Targets");
        }
            

        // ������������� �� ������� ��������
        var fsmExample = GameObject.FindGameObjectWithTag("Agent").GetComponent<FsmExample>();
        fsmExample.OnCollisionDetected += HandleCollision;  // �������� �� �������
    }

    public override void Update()
    {
        if (_targets.Length == 0) return;

        var target = _targets[_currentTargetIndex];

        // ���������, ��� ����� �� � ���� � ��������� ����� ����
        if (!_agent.pathPending && (_agent.destination != target.position))
        {
            _agent.SetDestination(target.position);  // ������������� ���� ��� ������
        }

        // ���������, ������ �� ����� ����
        if (Vector3.Distance(_agent.transform.position, target.position) < 0.1f)
        {
            Debug.Log($"Target {_currentTargetIndex} reached.");

            // ������� � ��������� ����
            _currentTargetIndex++;

            // ���� ��� ���� ��������� ����, ������������ � ������ ��� ���������
            if (_currentTargetIndex >= _targets.Length)
            {
                Debug.Log("All targets reached.");
                _currentTargetIndex = 0;  // �������� ����� � ������ ����

                // ���� ����������, ����������� ���������, ��������:
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

    }

}
