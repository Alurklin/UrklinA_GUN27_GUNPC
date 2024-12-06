using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SearchState : MonoBehaviour
{
    public Transform[] targets;  // Точки назначения
    private NavMeshAgent agent;
    private int currentTargetIndex = 0;
    private bool isMoving = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        // Проверка наличия целей в массиве
        if (targets == null || targets.Length == 0)
        {
            Debug.LogError("Нет целей для перемещения!");
            return;
        }

        // Начинаем движение к первой цели
        MoveToNextTarget();

    }

    void Update()
    {
        // Проверяем, достиг ли агент текущей цели
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance && agent.hasPath && !isMoving)
        {
            MoveToNextTarget();
        }
    }


    private void MoveToNextTarget()
    {
        if (targets.Length == 0) return;

        isMoving = true; // Блокируем повторный вызов, пока идет задержка
        StartCoroutine(MoveAfterDelay(5f));
    }

    private IEnumerator MoveAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        agent.SetDestination(targets[currentTargetIndex].position);

        Debug.Log($"Движение к цели: {targets[currentTargetIndex].name}");

        currentTargetIndex = (currentTargetIndex + 1) % targets.Length;

        isMoving = false;
    }

}

