using System;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
        if (scoreText != null)
        {
            scoreText2.text = "Round Score: " + rscore.ToString();
        }
    }

    public void ScoreCounter(int _rscore) 
    {
        if (_rscore == 10) 
        {
            Reset();
            Debug.Log("Strike!");
            strike += 2;
        }
        else if (_rscore < 10)
        {
            ballController.ResetBall();

            if (_rscore < 10)
            {
                Reset();
                Debug.Log("No!");
            }
            else if (_rscore == 10)
            {
                Reset();
                Debug.Log("Spare!");
                strike += 1;
            }
        }

        if (strike > 0)
        {
            _rscore = _rscore * 2;
        }

        score = score + _rscore;
        _rscore = 0;
        strike--;

        UpdateScoreDisplay();
        RoundUpdateScoreDisplay ();
    }

    public void Reset()
    {
        ballController.ResetBall();
        pin.ResetPin();
    }

}
