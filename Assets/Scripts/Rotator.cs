using UnityEngine;

public class Rotator : MonoBehaviour
{
    private float rotationSpeed = 50f; // �������� �������� � �������� � �������
    void Update()
    {
        // ������� ������ ������ ��� Y
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }
}
