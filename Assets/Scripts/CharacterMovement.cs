using UnityEngine;
using DG.Tweening;

public class CharacterMovement : MonoBehaviour
{
    [Header("Параметры движения")]
    public Transform[] pathPoints; // Точки пути
    public float duration = 3f;    // Продолжительность движения
    public Ease easeType = Ease.Linear; // Тип кривой анимации

    [Header("Дополнительные эффекты")]
    public bool scaleEffect = true; // Включить изменение масштаба
    public bool colorEffect = true; // Включить изменение цвета
    public GameObject dustEffectPrefab; // Эффект пыли или следов
    public float dustSpawnInterval = 0.1f; // Интервал спавна пыли

    private Vector3 lastPosition; // Последняя позиция персонажа

    private void Start()
    {
        // Создание массива позиций пути
        Vector3[] positions = new Vector3[pathPoints.Length];
        for (int i = 0; i < pathPoints.Length; i++)
        {
            positions[i] = pathPoints[i].position;
        }

        // Анимация движения по пути
        Sequence movementSequence = DOTween.Sequence();

        movementSequence.Append(transform.DOPath(positions, duration, PathType.CatmullRom)
            .SetEase(easeType)
            .OnStart(() => Debug.Log("Анимация началась"))
            .OnComplete(() => Debug.Log("Анимация завершена")));

        // Дополнительные эффекты
        if (scaleEffect)
        {
            movementSequence.Join(transform.DOScale(Vector3.one * 1.5f, duration / 2).SetLoops(2, LoopType.Yoyo));
        }

        if (colorEffect && TryGetComponent(out Renderer renderer))
        {
            movementSequence.Join(renderer.material.DOColor(Color.red, duration / 2).SetLoops(2, LoopType.Yoyo));
        }

        // Эффект пыли (спавн пыли в обратном направлении)
        if (dustEffectPrefab != null)
        {
            InvokeRepeating(nameof(SpawnDustEffect), 0, dustSpawnInterval);
        }

        // Запоминаем начальную позицию
        lastPosition = transform.position;
    }

    private void SpawnDustEffect()
    {
        Vector3 currentPosition = transform.position;

        // Вычисляем направление движения
        Vector3 movementDirection = (currentPosition - lastPosition).normalized;

        // Инвертируем направление для эффекта пыли
        Vector3 dustDirection = -movementDirection;

        // Создаём эффект пыли
        if (movementDirection != Vector3.zero) // Избегаем деления на ноль при остановке
        {
            GameObject dust = Instantiate(dustEffectPrefab, transform.position, Quaternion.LookRotation(dustDirection));
            Destroy(dust, 2f); // Уничтожаем эффект через 2 секунды
        }

        // Обновляем последнюю позицию
        lastPosition = currentPosition;
    }
}
