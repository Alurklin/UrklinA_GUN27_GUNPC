using UnityEngine;
using System.Collections;
using System;

public class BallController : MonoBehaviour
{
    public float throwForce = 10f;  // Сила броска
    private Rigidbody _rb;// Ссылка на компонент Rigidbody мяча
    public float moveRange = 5f;    // Диапазон движения (расстояние между точками)
    public float moveSpeed = 3f;    // Скорость движения
    private bool _isThrow = false; //брошен ли мяч
    [NonSerialized]
    public bool _secondChance = true; //проверка, нужно ли перезапустить корутину

    private Vector3 _bInitialPosition; // Начальная позиция шара
    private Quaternion _bInitialRotation; // Начальная ориентация шара
    private Rigidbody _ballRigidbody; // Rigidbody шара

    public ScoreManager scoreManager;

    void Start()
    {
        if (scoreManager == null)
        {
            scoreManager = FindObjectOfType<ScoreManager>();  // Если не присвоена ссылка, найдем ScoreManager в сцене
        }

        _bInitialPosition = transform.position;
        _bInitialRotation = transform.rotation;
        // Получаем компонент Rigidbody мяча
        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (_isThrow == false)
        {
            // Используем Mathf.PingPong для зацикленного движения
            float movement = Mathf.PingPong(Time.time * moveSpeed, moveRange * 2) - moveRange;

            // Перемещаем объект по оси X
            transform.position = new Vector3(transform.position.x, transform.position.y, movement);
        }
        

        // Проверяем, если нажата левая кнопка мыши
        if (Input.GetMouseButtonDown(0))
        {
            _isThrow = true;
            // Определяем направление броска (например, вперед)
            Vector3 throwDirection = transform.forward;  // Мяч будет двигаться вперед относительно его локального положения
            _rb.AddForce(throwDirection * throwForce, ForceMode.Impulse);  // Применяем силу

            if (_secondChance)
            {
                scoreManager.StartScoring();
            } 
        }

    }

    public void ResetBall()
    {
        // Останавливаем движение кегли
        _rb.velocity = Vector3.zero;
        _rb.angularVelocity = Vector3.zero;

        // Возвращаем её в начальную позицию и ориентацию
        transform.position = _bInitialPosition;
        transform.rotation = _bInitialRotation;

        // Отключаем физику на момент возврата, чтобы избежать коллизий
        _rb.isKinematic = true;

        // Включаем физику обратно через кадр
        Invoke(nameof(BEnablePhysics), 0.1f);
        _isThrow = false;
    }

    public void BEnablePhysics()
    {
        _rb.isKinematic = false;
    }
}
