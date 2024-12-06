using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

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
        yield return new WaitForSeconds(5f); // �������� 5 ������

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

            yield return new WaitForSeconds(5f); // �������� ��� ������ �������

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

        // ���� Strike ��� Spare
        if (strike > 0)
        {
            rscore *= 2;
        }

        score += rscore;
        rscore = 0;  // ����� ����� ������
        strike = Mathf.Max(0, strike - 1); // ��������� strike, �� �� ���� 0

        UpdateScoreDisplay();
        RoundUpdateScoreDisplay();
        Reset(); // ���������� ��� � �����
    }

    public void Reset()
    {
        ballController.ResetBall();
        pin.ResetAllPins();
        ballController._secondChance = true;
    }

}
