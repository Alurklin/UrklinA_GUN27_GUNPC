using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float rotationSpeed = 50f; // Скорость вращения в градусах в секунду

    void Update()
    {
        // Вращаем объект вокруг оси Y
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }
}
