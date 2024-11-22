using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownPlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // �������� �������� ���������

    void Update()
    {
        // �������� �������� ����� �� ����������� � ���������
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // ������� ������ ��������
        Vector3 movement = new Vector3(horizontal, vertical, 0f);

        // ���������� ���������
        transform.Translate(movement * moveSpeed * Time.deltaTime, Space.World);

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("Ttttttt");
        }
    }
}
