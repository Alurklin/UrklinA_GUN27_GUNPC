using UnityEngine;

public class Rotator : MonoBehaviour
{
    // ���� ��� ��������� �������� ����� ���������
    public Vector3 _rotate = new Vector3(0, 0, 0);

    // �������������� ���������� ���� �������
    private Rigidbody _rigidbody;

    // ������������ ����� Start
    void Start()
    {
        // �������� ��������� Rigidbody
        _rigidbody = GetComponent<Rigidbody>();

        // ������������� ������ ��� ��������������
        _rigidbody.isKinematic = true;

        // ��������� �������� � ����������� �����
        StartCoroutine(RotateObject());
    }

    // ����� ��� ������������ �������� �������
    private System.Collections.IEnumerator RotateObject()
    {
        while (true) // ����������� ����
        {
            // ������� ������ � ������ ������� ��� ��������� ��������
            _rigidbody.MoveRotation(_rigidbody.rotation * Quaternion.Euler(_rotate * Time.deltaTime));

            // ���� ����� Unity ��� ���������� ������
            yield return null;
        }
    }
}
