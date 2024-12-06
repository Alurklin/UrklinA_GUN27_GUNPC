using UnityEngine;
using System.Collections;
using System;

public class BallController : MonoBehaviour
{
    public float throwForce = 10f;  // ���� ������
    private Rigidbody _rb;// ������ �� ��������� Rigidbody ����
    public float moveRange = 5f;    // �������� �������� (���������� ����� �������)
    public float moveSpeed = 3f;    // �������� ��������
    private bool _isThrow = false; //������ �� ���
    [NonSerialized]
    public bool _secondChance = true; //��������, ����� �� ������������� ��������

    private Vector3 _bInitialPosition; // ��������� ������� ����
    private Quaternion _bInitialRotation; // ��������� ���������� ����
    private Rigidbody _ballRigidbody; // Rigidbody ����

    public ScoreManager scoreManager;

    void Start()
    {
        if (scoreManager == null)
        {
            scoreManager = FindObjectOfType<ScoreManager>();  // ���� �� ��������� ������, ������ ScoreManager � �����
        }

        _bInitialPosition = transform.position;
        _bInitialRotation = transform.rotation;
        // �������� ��������� Rigidbody ����
        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (_isThrow == false)
        {
            // ���������� Mathf.PingPong ��� ������������ ��������
            float movement = Mathf.PingPong(Time.time * moveSpeed, moveRange * 2) - moveRange;

            // ���������� ������ �� ��� X
            transform.position = new Vector3(transform.position.x, transform.position.y, movement);
        }
        

        // ���������, ���� ������ ����� ������ ����
        if (Input.GetMouseButtonDown(0))
        {
            _isThrow = true;
            // ���������� ����������� ������ (��������, ������)
            Vector3 throwDirection = transform.forward;  // ��� ����� ��������� ������ ������������ ��� ���������� ���������
            _rb.AddForce(throwDirection * throwForce, ForceMode.Impulse);  // ��������� ����

            if (_secondChance)
            {
                scoreManager.StartScoring();
            } 
        }

    }

    public void ResetBall()
    {
        // ������������� �������� �����
        _rb.velocity = Vector3.zero;
        _rb.angularVelocity = Vector3.zero;

        // ���������� � � ��������� ������� � ����������
        transform.position = _bInitialPosition;
        transform.rotation = _bInitialRotation;

        // ��������� ������ �� ������ ��������, ����� �������� ��������
        _rb.isKinematic = true;

        // �������� ������ ������� ����� ����
        Invoke(nameof(BEnablePhysics), 0.1f);
        _isThrow = false;
    }

    public void BEnablePhysics()
    {
        _rb.isKinematic = false;
    }
}
