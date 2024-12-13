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
        _animator = animator;  // Сохраняем Animator
        _agent = GameObject.FindGameObjectWithTag("Agent").GetComponent<NavMeshAgent>();  // Получаем компонент NavMeshAgent
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
            

        // Подписываемся на событие коллизии
        var fsmExample = GameObject.FindGameObjectWithTag("Agent").GetComponent<FsmExample>();
        fsmExample.OnCollisionDetected += HandleCollision;  // Подписка на событие
    }

    public override void Update()
    {
        if (_targets.Length == 0) return;

        var target = _targets[_currentTargetIndex];

        // Проверяем, что агент не в пути и назначаем новую цель
        if (!_agent.pathPending && (_agent.destination != target.position))
        {
            _agent.SetDestination(target.position);  // Устанавливаем цель для агента
        }

        // Проверяем, достиг ли агент цели
        if (Vector3.Distance(_agent.transform.position, target.position) < 0.1f)
        {
            Debug.Log($"Target {_currentTargetIndex} reached.");

            // Переход к следующей цели
            _currentTargetIndex++;

            // Если это была последняя цель, возвращаемся к первой или завершаем
            if (_currentTargetIndex >= _targets.Length)
            {
                Debug.Log("All targets reached.");
                _currentTargetIndex = 0;  // Начинаем снова с первой цели

                // Если необходимо, переключаем состояние, например:
                Fsm.SetState<FsmStateIdle>();
            }
        }

    }

    public override void Exit()
    {
        // Отписываемся от события, когда выходим из состояния
        var fsmExample = GameObject.FindGameObjectWithTag("Agent").GetComponent<FsmExample>();
        fsmExample.OnCollisionDetected -= HandleCollision;
    }

    private void HandleCollision(GameObject target)
    {
        Debug.Log("Handling collision with target: " + target.name);

    }

}
