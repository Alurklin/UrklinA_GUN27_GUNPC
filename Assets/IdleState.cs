
using UnityEngine;

public class IdleState : MonoBehaviour
{
    private Animator animator;
    private float idleTimer;

    void Start()
    {
        animator = GetComponent<Animator>();

        EnterIdle();
    }

    void Update()
    {
        idleTimer += Time.deltaTime;

        if (idleTimer > 5f)
        {
            animator.SetFloat("TimeInIdle", idleTimer);
        }

    }

    public void EnterIdle()
    {
        idleTimer = 0f;
        animator.SetBool("FoundItem", false);
        animator.SetBool("Collected", false);
    }
}
