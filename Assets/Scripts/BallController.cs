using UnityEngine;

public class BallController : MonoBehaviour
{
    public float throwForce = 10f;  // ���� ������
    private Rigidbody rb;// ������ �� ��������� Rigidbody ����
    public float moveRange = 5f;    // �������� �������� (���������� ����� �������)
    public float moveSpeed = 3f;    // �������� ��������
    private bool _isThrow = false; //������ �� ���

    void Start()
    {
        // �������� ��������� Rigidbody ����
        rb = GetComponent<Rigidbody>();
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
            rb.AddForce(throwDirection * throwForce, ForceMode.Impulse);  // ��������� ����
        }

    }
}
