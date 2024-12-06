using UnityEngine;

public class BallController : MonoBehaviour
{
    public float throwForce = 10f;  // Сила броска
    private Rigidbody rb;// Ссылка на компонент Rigidbody мяча
    public float moveRange = 5f;    // Диапазон движения (расстояние между точками)
    public float moveSpeed = 3f;    // Скорость движения
    private bool _isThrow = false; //брошен ли мяч

    void Start()
    {
        // Получаем компонент Rigidbody мяча
        rb = GetComponent<Rigidbody>();
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
            rb.AddForce(throwDirection * throwForce, ForceMode.Impulse);  // Применяем силу
        }

    }
}
