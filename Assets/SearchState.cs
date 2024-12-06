using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SearchState : MonoBehaviour
{
    public Transform[] targets;  // ����� ����������
    private NavMeshAgent agent;
    private int currentTargetIndex = 0;
    private bool isMoving = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        // �������� ������� ����� � �������
        if (targets == null || targets.Length == 0)
        {
            Debug.LogError("��� ����� ��� �����������!");
            return;
        }

        // �������� �������� � ������ ����
        MoveToNextTarget();

    }

    void Update()
    {
        // ���������, ������ �� ����� ������� ����
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance && agent.hasPath && !isMoving)
        {
            MoveToNextTarget();
        }
    }


    private void MoveToNextTarget()
    {
        if (targets.Length == 0) return;

        isMoving = true; // ��������� ��������� �����, ���� ���� ��������
        StartCoroutine(MoveAfterDelay(5f));
    }

    private IEnumerator MoveAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        agent.SetDestination(targets[currentTargetIndex].position);

        Debug.Log($"�������� � ����: {targets[currentTargetIndex].name}");

        currentTargetIndex = (currentTargetIndex + 1) % targets.Length;

        isMoving = false;
    }

}

