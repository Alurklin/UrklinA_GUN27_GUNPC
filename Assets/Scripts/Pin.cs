using UnityEngine;

public class Pin : MonoBehaviour
{
    public ScoreManager scoreManager;  // Ссылка на объект, который управляет подсчетом очков
    private bool isKnockedDown = false; // Флаг, чтобы убедиться, что очко не будет засчитано дважды

    void Start()
    {

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
}
