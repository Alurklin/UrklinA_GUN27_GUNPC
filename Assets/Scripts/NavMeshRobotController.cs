using UnityEngine;
using UnityEngine.AI;

public class NavMeshRobotController : MonoBehaviour
{
    public Transform[] targets;  // ����� ����������
    private NavMeshAgent agent;
    private int currentTargetIndex = 0;
    public Vector3 roomCenter;  // ����� ������� ��� ���������� ��������
    public float roomRadius = 10f;  // ������ ���� ���������� ��������
    private bool isRandomMovement = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        // ��������, ��� ����� �������� �� NavMesh
        NavMeshHit hit;
        if (NavMesh.SamplePosition(transform.position, out hit, 5f, NavMesh.AllAreas))
        {
            transform.position = hit.position;
        }
        else
        {
            Debug.LogError("����� �� ��������� ����� � NavMesh!");
        }

        // ������� ��� null-������� �� ������� �����
        targets = System.Array.FindAll(targets, t => t != null);

        if (targets.Length > 0)
        {
            MoveToNextTarget();
        }
        else
        {
            StartRandomMovement();
        }
    }

    void Update()
    {
        if (agent.isOnNavMesh)
        {
            // ���� ����� ��������� � ������ ���������� ��������
            if (isRandomMovement)
            {
                if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
                {
                    MoveToRandomTarget();
                }
            }
            // ���� ����� �������� � �������� �����
            else
            {
                if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
                {
                    MoveToNextTarget();
                }
            }
        }
        else
        {
            Debug.LogWarning("NavMeshAgent �� ��������� �� NavMesh!");
        }
    }

    private void MoveToNextTarget()
    {
        if (targets.Length == 0)
        {
            StartRandomMovement();
            return;
        }

        // ��������� ������� ���� �� null
        while (targets[currentTargetIndex] == null)
        {
            currentTargetIndex = (currentTargetIndex + 1) % targets.Length;

            if (currentTargetIndex == 0)
            {
                StartRandomMovement();
                return;
            }
        }

        agent.SetDestination(targets[currentTargetIndex].position);
        currentTargetIndex = (currentTargetIndex + 1) % targets.Length;
    }

    private void StartRandomMovement()
    {
        Debug.Log("������� � ����� ���������� ��������.");
        isRandomMovement = true;
        MoveToRandomTarget();
    }

    private void MoveToRandomTarget()
    {
        Vector3 randomTarget = GetRandomPosition();
        agent.SetDestination(randomTarget);
    }

    private Vector3 GetRandomPosition()
    {
        // ���������� ��������� ����� � �������� �������
        Vector3 randomDirection = Random.insideUnitSphere * roomRadius;
        randomDirection += roomCenter;  // ������� ������������ ������ �������
        randomDirection.y = transform.position.y;  // ��������� ������ ������

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, roomRadius, NavMesh.AllAreas))
        {
            return hit.position;  // ���������� ������� �� NavMesh
        }

        return transform.position;  // ���� �� �������, ���������� ������� �������
    }
}
