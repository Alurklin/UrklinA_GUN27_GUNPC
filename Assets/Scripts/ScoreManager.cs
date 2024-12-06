using UnityEngine;
using UnityEngine.UI;  // ��� ������ � UI (���� �� ������ ���������� ���� �� ������)

public class ScoreManager : MonoBehaviour
{
    public Text scoreText;  // UI ����� ��� ����������� �����
    private int score = 0;  // ������� ���������� �����

    void Start()
    {
        UpdateScoreDisplay();  // ��������� ����������� ����� ��� ������ ����
    }

    // ����� ��� ���������� �����
    public void AddScore(int points)
    {
        score += points;  // ��������� ����
        UpdateScoreDisplay();  // ��������� ����������� �����
    }

    // ����� ��� ���������� UI ���������� ���� � ������
    private void UpdateScoreDisplay()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score.ToString();
        }
    }
}
