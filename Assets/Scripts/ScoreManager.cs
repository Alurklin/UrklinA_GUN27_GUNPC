using UnityEngine;
using UnityEngine.UI;  // Для работы с UI (если вы хотите показывать очки на экране)

public class ScoreManager : MonoBehaviour
{
    public Text scoreText;  // UI текст для отображения очков
    private int score = 0;  // Текущее количество очков

    void Start()
    {
        UpdateScoreDisplay();  // Обновляем отображение очков при старте игры
    }

    // Метод для добавления очков
    public void AddScore(int points)
    {
        score += points;  // Добавляем очки
        UpdateScoreDisplay();  // Обновляем отображение очков
    }

    // Метод для обновления UI текстового поля с очками
    private void UpdateScoreDisplay()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score.ToString();
        }
    }
}
