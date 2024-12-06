using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
    public Text scoreText;  // UI текст для отображения очков
    public Text scoreText2; // очки за раунд
    [NonSerialized]
    public int score = 0;  // Текущее количество очков
    [NonSerialized]
    public int rscore = 0; // Количество очков в раунде
    [NonSerialized]
    public int strike = 0; // Количество раундов с удвоением очков

    private BallController ballController;
    private Pin pin;

    void Start()
    {
        if (pin == null)
        {
            pin = FindObjectOfType<Pin>();  // Если не присвоена ссылка, найдем Pin в сцене
        }

        if (ballController == null)
        {
            ballController = FindObjectOfType<BallController>();  // Если не присвоена ссылка, найдем Ball Controller в сцене
        }

        UpdateScoreDisplay();  // Обновляем отображение очков при старте игры
        RoundUpdateScoreDisplay();
    }

    // Метод для добавления очков
    public void AddScore(int points)
    {
        rscore += points;  // Добавляем очки
        RoundUpdateScoreDisplay();  // Обновляем отображение очков
    }

    // Метод для обновления UI текстового поля с очками
    private void UpdateScoreDisplay()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score.ToString();
        }
    }

    // Метод для обновления UI текстового поля с очками в раунде
    private void RoundUpdateScoreDisplay()
    {
        if (scoreText2 != null)
        {
            scoreText2.text = "Round Score: " + rscore.ToString();
        }
    }

    public void StartScoring()
    {
        StartCoroutine(ScoreCalculation());
    }

    private IEnumerator ScoreCalculation()
    {
        yield return new WaitForSeconds(5f); // Задержка 5 секунд

        if (rscore == 10)
        {
            Debug.Log("Strike!");
            strike += 2;
        }
        else if (rscore < 10)
        {
            Debug.Log("Second Chance!");
            ballController._secondChance = false;
            ballController.ResetBall();

            yield return new WaitForSeconds(5f); // Задержка для второй попытки

            if (rscore == 10)
            {
                Debug.Log("Spare!");
                strike += 1;
            }
            else
            {
                Debug.Log("Miss!");
            }
        }

        // Учет Strike или Spare
        if (strike > 0)
        {
            rscore *= 2;
        }

        score += rscore;
        rscore = 0;  // Сброс очков раунда
        strike = Mathf.Max(0, strike - 1); // Уменьшаем strike, но не ниже 0

        UpdateScoreDisplay();
        RoundUpdateScoreDisplay();
        Reset(); // Сбрасываем шар и кегли
    }

    public void Reset()
    {
        ballController.ResetBall();
        pin.ResetAllPins();
        ballController._secondChance = true;
    }

}
