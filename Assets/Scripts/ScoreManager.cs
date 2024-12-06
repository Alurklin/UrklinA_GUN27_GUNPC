using System;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text scoreText;  // UI ����� ��� ����������� �����
    public Text scoreText2; // ���� �� �����
    [NonSerialized]
    public int score = 0;  // ������� ���������� �����
    [NonSerialized]
    public int rscore = 0; // ���������� ����� � ������
    [NonSerialized]
    public int strike = 0; // ���������� ������� � ��������� �����

    private BallController ballController;
    private Pin pin;

    void Start()
    {
        if (pin == null)
        {
            pin = FindObjectOfType<Pin>();  // ���� �� ��������� ������, ������ Pin � �����
        }

        if (ballController == null)
        {
            ballController = FindObjectOfType<BallController>();  // ���� �� ��������� ������, ������ Ball Controller � �����
        }

        UpdateScoreDisplay();  // ��������� ����������� ����� ��� ������ ����
        RoundUpdateScoreDisplay();
    }

    // ����� ��� ���������� �����
    public void AddScore(int points)
    {
        rscore += points;  // ��������� ����
        RoundUpdateScoreDisplay();  // ��������� ����������� �����
    }

    // ����� ��� ���������� UI ���������� ���� � ������
    private void UpdateScoreDisplay()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score.ToString();
        }
    }

    // ����� ��� ���������� UI ���������� ���� � ������ � ������
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
