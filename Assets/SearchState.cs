using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SearchState : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator animator;
    private CollectState _collect;

    public Transform[] targets; // Список целей, заданных через Inspector

    private int currentTargetIndex = 0; // Индекс текущей цели

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        if (targets.Length > 0)
        {
            // Устанавливаем первую цель
            MoveToNextTarget();
        }
        else
        {
            Debug.LogWarning("Массив целей пуст! Добавьте объекты в массив targets.");
        }
    }

    void Update()
    {
        // Проверяем, достиг ли агент текущей цели
        if (agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
        {
            // Переход к следующей цели
            MoveToNextTarget();
        }
    }

    void MoveToNextTarget()
    {
        if (targets.Length == 0) 
        {
            _collect.enabled = true;
            return;
        }
        

        // Устанавливаем цель и запускаем анимацию
        agent.SetDestination(targets[currentTargetIndex].position);
        animator.SetBool("FoundItem", true);

        // Переход к следующей цели
        currentTargetIndex = (currentTargetIndex + 1) % targets.Length;
    }
}

