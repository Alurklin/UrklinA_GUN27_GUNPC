using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectState : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void CollectItem(GameObject item)
    {
        StartCoroutine(CollectRoutine(item));
    }

    private IEnumerator CollectRoutine(GameObject item)
    {
        yield return new WaitForSeconds(2f); // �������� �� ����
        Destroy(item); // ������� �������
        animator.SetBool("Collected", true);
    }
}
