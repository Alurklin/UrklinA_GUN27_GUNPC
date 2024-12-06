using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SearchState : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator animator;
    private CollectState _collect;

    public Transform[] targets; // ������ �����, �������� ����� Inspector

    private int currentTargetIndex = 0; // ������ ������� ����

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        if (targets.Length > 0)
        {
            // ������������� ������ ����
            MoveToNextTarget();
        }
        else
        {
            Debug.LogWarning("������ ����� ����! �������� ������� � ������ targets.");
        }
    }

    void Update()
    {
        // ���������, ������ �� ����� ������� ����
        if (agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
        {
            // ������� � ��������� ����
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
        

        // ������������� ���� � ��������� ��������
        agent.SetDestination(targets[currentTargetIndex].position);
        animator.SetBool("FoundItem", true);

        // ������� � ��������� ����
        currentTargetIndex = (currentTargetIndex + 1) % targets.Length;
    }
}

