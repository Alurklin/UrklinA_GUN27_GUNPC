using UnityEngine;

public class Pin : MonoBehaviour
{
    public ScoreManager scoreManager;  // Ссылка на объект, который управляет подсчетом очков
    private bool isKnockedDown = false; // Флаг, чтобы убедиться, что очко не будет засчитано дважды

    private Vector3 _initialPosition; // Начальная позиция кегли
    private Quaternion _initialRotation; // Начальная ориентация кегли
    private Rigidbody _pinRigidbody; // Rigidbody кегли

    void Start()
    {
        // Сохраняем начальную позицию и ориентацию
        _initialPosition = transform.position;
        _initialRotation = transform.rotation;

        // Получаем Rigidbody кегли
        _pinRigidbody = GetComponent<Rigidbody>();

        if (scoreManager == null)
        {
            scoreManager = FindObjectOfType<ScoreManager>();  // Если не присвоена ссылка, найдем ScoreManager в сцене
        }

        if (scoreManager == null)
        {
            Debug.LogError("ScoreManager не найден! Убедитесь, что объект ScoreManager присутствует в сцене.");
        }
    }

    void Update()
    {
        // Проверяем, упала ли кегля (по оси Y, например, если она слишком низко)
        if (!isKnockedDown && transform.position.y < 0.9f) // Порог для падения кегли (в зависимости от масштаба)
        {
            KnockDown();
        }
    }

    // Засчитываем очко, когда кегля падает
    void KnockDown()
    {
        isKnockedDown = true; // Устанавливаем флаг, чтобы не засчитывать очко несколько раз
        scoreManager.AddScore(1);  // Засчитываем 1 очко
    }

    public void ResetPin()
    {
        // Останавливаем движение кегли
        _pinRigidbody.velocity = Vector3.zero;
        _pinRigidbody.angularVelocity = Vector3.zero;

        // Возвращаем её в начальную позицию и ориентацию
        transform.position = _initialPosition;
        transform.rotation = _initialRotation;

        // Отключаем физику на момент возврата, чтобы избежать коллизий
        _pinRigidbody.isKinematic = true;

        // Включаем физику обратно через кадр
        Invoke(nameof(EnablePhysics), 0.1f);
    }

    public void EnablePhysics()
    {
        _pinRigidbody.isKinematic = false;
    }
}
