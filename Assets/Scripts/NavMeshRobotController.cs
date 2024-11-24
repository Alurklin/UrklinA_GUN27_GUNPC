using UnityEngine;
using UnityEngine.AI;

public class NavMeshRobotController : MonoBehaviour
{
    public Transform[] targets;  // Точки назначения
    private NavMeshAgent agent;
    private int currentTargetIndex = 0;
    public Vector3 roomCenter;  // Центр комнаты для случайного движения
    public float roomRadius = 10f;  // Радиус зоны случайного движения
    private bool isRandomMovement = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        // Убедимся, что робот стартует на NavMesh
        NavMeshHit hit;
        if (NavMesh.SamplePosition(transform.position, out hit, 5f, NavMesh.AllAreas))
        {
            transform.position = hit.position;
        }
        else
        {
            Debug.LogError("Робот не находится рядом с NavMesh!");
        }

        // Убираем все null-объекты из массива целей
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
            // Если робот находится в режиме случайного движения
            if (isRandomMovement)
            {
                if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
                {
                    MoveToRandomTarget();
                }
            }
            // Если робот движется к заданным целям
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
            Debug.LogWarning("NavMeshAgent не находится на NavMesh!");
        }
    }

    private void MoveToNextTarget()
    {
        if (targets.Length == 0)
        {
            StartRandomMovement();
            return;
        }

        // Проверяем текущую цель на null
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
        Debug.Log("Переход в режим случайного движения.");
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
        // Генерируем случайную точку в пределах комнаты
        Vector3 randomDirection = Random.insideUnitSphere * roomRadius;
        randomDirection += roomCenter;  // Смещаем относительно центра комнаты
        randomDirection.y = transform.position.y;  // Сохраняем высоту робота

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, roomRadius, NavMesh.AllAreas))
        {
            return hit.position;  // Возвращаем позицию на NavMesh
        }

        return transform.position;  // Если не удалось, возвращаем текущую позицию
    }
}
