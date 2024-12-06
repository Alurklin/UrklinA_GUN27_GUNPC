using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float rotationSpeed = 50f; // �������� �������� � �������� � �������

    void Update()
    {
        // ������� ������ ������ ��� Y
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }
}
