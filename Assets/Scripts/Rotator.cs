using UnityEngine;

public class Rotator : MonoBehaviour
{
    // Поле для настройки вращения через инспектор
    public Vector3 _rotate = new Vector3(0, 0, 0);

    // Кинематическое физическое тело объекта
    private Rigidbody _rigidbody;

    // Итерационный метод Start
    void Start()
    {
        // Получаем компонент Rigidbody
        _rigidbody = GetComponent<Rigidbody>();

        // Устанавливаем объект как кинематический
        _rigidbody.isKinematic = true;

        // Запускаем вращение в бесконечном цикле
        StartCoroutine(RotateObject());
    }

    // Метод для бесконечного вращения объекта
    private System.Collections.IEnumerator RotateObject()
    {
        while (true) // Бесконечный цикл
        {
            // Вращаем объект с учетом времени для плавности вращения
            _rigidbody.MoveRotation(_rigidbody.rotation * Quaternion.Euler(_rotate * Time.deltaTime));

            // Даем время Unity для обновления кадров
            yield return null;
        }
    }
}
