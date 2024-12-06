using UnityEngine;

public class Pin : MonoBehaviour
{
    public ScoreManager scoreManager;  // ������ �� ������, ������� ��������� ��������� �����
    private bool isKnockedDown = false; // ����, ����� ���������, ��� ���� �� ����� ��������� ������

    private Vector3 _initialPosition; // ��������� ������� �����
    private Quaternion _initialRotation; // ��������� ���������� �����
    private Rigidbody _pinRigidbody; // Rigidbody �����

    void Start()
    {
        // ��������� ��������� ������� � ����������
        _initialPosition = transform.position;
        _initialRotation = transform.rotation;

        // �������� Rigidbody �����
        _pinRigidbody = GetComponent<Rigidbody>();

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

    public void ResetPin()
    {
        // ������������� �������� �����
        _pinRigidbody.velocity = Vector3.zero;
        _pinRigidbody.angularVelocity = Vector3.zero;

        // ���������� � � ��������� ������� � ����������
        transform.position = _initialPosition;
        transform.rotation = _initialRotation;

        // ��������� ������ �� ������ ��������, ����� �������� ��������
        _pinRigidbody.isKinematic = true;

        // �������� ������ ������� ����� ����
        Invoke(nameof(EnablePhysics), 0.1f);
    }

    public void EnablePhysics()
    {
        _pinRigidbody.isKinematic = false;
    }
}
