using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotController : MonoBehaviour
{
    public float speed = 3f;  // Скорость движения
    public float rotationSpeed = 100f;  // Скорость поворота
    public float rayDistance = 1f;  // Длина лучей Raycast
    public float cleaningRadius = 1f;// Радиус сбора мусора

    public AudioClip movementSound;
    public AudioClip collisionSound;
    private AudioSource audioSource;

    private Vector3 randomDirection;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (movementSound != null)
        {
            audioSource.clip = movementSound;
            audioSource.Play();
        }

        SetRandomDirection();
    }

    void Update()
    {
        MoveForward();
        Clean();

        // Проверяем наличие препятствий с помощью Raycast
        if (Physics.Raycast(transform.position, transform.forward, rayDistance))
        {
            ChangeDirection();  // Если впереди препятствие, меняем направление
        }
        else if (Physics.Raycast(transform.position, transform.right, rayDistance))
        {
            ChangeDirection();  // Если справа препятствие, меняем направление
        }
        else if (Physics.Raycast(transform.position, -transform.right, rayDistance))
        {
            ChangeDirection();  // Если слева препятствие, меняем направление
        }
    }

    private void MoveForward()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void ChangeDirection()
    {
        // Поворачиваемся на случайный угол
        float randomAngle = Random.Range(90, 270);
        transform.Rotate(Vector3.up, randomAngle);
    }

    private void SetRandomDirection()
    {
        // Выбираем случайное направление
        randomDirection = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
    }

    void Clean()
    {
        Collider[] trash = Physics.OverlapSphere(transform.position, cleaningRadius);
        foreach (Collider item in trash)
        {
            if (item.CompareTag("Trash"))
            {
                audioSource.PlayOneShot(collisionSound);
                Destroy(item.gameObject);  // Убираем мусор
            }
        }
    }
}
