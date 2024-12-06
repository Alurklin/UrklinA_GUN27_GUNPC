using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectState : MonoBehaviour
{
    private Animator animator;
    private IdleState _idleState;

    void Start()
    {
        animator = GetComponent<Animator>();
        _idleState = GetComponent<IdleState>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // ���������, ���� �� �� ������������� ������� ��������� CoinMarker
        CoinMarker coin = collision.gameObject.GetComponent<CoinMarker>();
        if (coin != null)
        {
            Destroy(collision.gameObject); // ������� �������
            StartCoroutine(CoinsGrab(2f));
        }
    }

    private IEnumerator CoinsGrab(float delay)
    {
        animator.SetBool("FoundItem", true);

        yield return new WaitForSeconds(delay);

        Debug.Log("������ �������!");
        animator.SetBool("Collected", true);
        _idleState.EnterIdle();
    }

}
