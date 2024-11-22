using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownPlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // Скорость движения персонажа

    void Update()
    {
        // Получаем значения ввода по горизонтали и вертикали
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Создаем вектор движения
        Vector3 movement = new Vector3(horizontal, vertical, 0f);

        // Перемещаем персонажа
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
