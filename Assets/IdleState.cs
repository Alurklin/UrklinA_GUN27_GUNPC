using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IdleState : MonoBehaviour
{
    private Animator animator;
    private NavMeshAgent agent;
    private float idleTimer;

    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        if (agent != null)
        {
            agent.enabled = false; // ��������� NavMeshAgent � ������
        }

        EnterIdle();
    }

    void Update()
    {
        idleTimer += Time.deltaTime;

        if (idleTimer > 5f)
        {
            animator.SetFloat("TimeInIdle", idleTimer);

            if (agent != null && !agent.enabled)
            {
                agent.enabled = true; // �������� NavMeshAgent ����� 5 ������
                Debug.Log("NavMeshAgent �������");
            }
        }
    }

    public void EnterIdle()
    {
        idleTimer = 0f;
        animator.SetBool("FoundItem", false);
        animator.SetBool("Collected", false);
    }
}
