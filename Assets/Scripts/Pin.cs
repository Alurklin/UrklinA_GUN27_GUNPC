using UnityEngine;

public class Pin : MonoBehaviour
{
    public ScoreManager scoreManager;  // ������ �� ������, ������� ��������� ��������� �����
    private bool isKnockedDown = false; // ����, ����� ���������, ��� ���� �� ����� ��������� ������

    void Start()
    {

        if (scoreManager == null)
        {
            scoreManager = FindObjectOfType<ScoreManager>();  // ���� �� ��������� ������, ������ ScoreManager � �����
        }

        if (scoreManager == null)
        {
            Debug.LogError("ScoreManager �� ������! ���������, ��� ������ ScoreManager ������������ � �����.");
        }
    }

    void Update()
    {
        // ���������, ����� �� ����� (�� ��� Y, ��������, ���� ��� ������� �����)
        if (!isKnockedDown && transform.position.y < 0.9f) // ����� ��� ������� ����� (� ����������� �� ��������)
        {
            KnockDown();
        }
    }

    // ����������� ����, ����� ����� ������
    void KnockDown()
    {
        isKnockedDown = true; // ������������� ����, ����� �� ����������� ���� ��������� ���
        scoreManager.AddScore(1);  // ����������� 1 ����
    }
}
