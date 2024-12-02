using UnityEngine;
using DG.Tweening;

public class CharacterMovement : MonoBehaviour
{
    [Header("��������� ��������")]
    public Transform[] pathPoints; // ����� ����
    public float duration = 3f;    // ����������������� ��������
    public Ease easeType = Ease.Linear; // ��� ������ ��������

    [Header("�������������� �������")]
    public bool scaleEffect = true; // �������� ��������� ��������
    public bool colorEffect = true; // �������� ��������� �����
    public GameObject dustEffectPrefab; // ������ ���� ��� ������
    public float dustSpawnInterval = 0.1f; // �������� ������ ����

    private Vector3 lastPosition; // ��������� ������� ���������

    private void Start()
    {
        // �������� ������� ������� ����
        Vector3[] positions = new Vector3[pathPoints.Length];
        for (int i = 0; i < pathPoints.Length; i++)
        {
            positions[i] = pathPoints[i].position;
        }

        // �������� �������� �� ����
        Sequence movementSequence = DOTween.Sequence();

        movementSequence.Append(transform.DOPath(positions, duration, PathType.CatmullRom)
            .SetEase(easeType)
            .OnStart(() => Debug.Log("�������� ��������"))
            .OnComplete(() => Debug.Log("�������� ���������")));

        // �������������� �������
        if (scaleEffect)
        {
            movementSequence.Join(transform.DOScale(Vector3.one * 1.5f, duration / 2).SetLoops(2, LoopType.Yoyo));
        }

        if (colorEffect && TryGetComponent(out Renderer renderer))
        {
            movementSequence.Join(renderer.material.DOColor(Color.red, duration / 2).SetLoops(2, LoopType.Yoyo));
        }

        // ������ ���� (����� ���� � �������� �����������)
        if (dustEffectPrefab != null)
        {
            InvokeRepeating(nameof(SpawnDustEffect), 0, dustSpawnInterval);
        }

        // ���������� ��������� �������
        lastPosition = transform.position;
    }

    private void SpawnDustEffect()
    {
        Vector3 currentPosition = transform.position;

        // ��������� ����������� ��������
        Vector3 movementDirection = (currentPosition - lastPosition).normalized;

        // ����������� ����������� ��� ������� ����
        Vector3 dustDirection = -movementDirection;

        // ������ ������ ����
        if (movementDirection != Vector3.zero) // �������� ������� �� ���� ��� ���������
        {
            GameObject dust = Instantiate(dustEffectPrefab, transform.position, Quaternion.LookRotation(dustDirection));
            Destroy(dust, 2f); // ���������� ������ ����� 2 �������
        }

        // ��������� ��������� �������
        lastPosition = currentPosition;
    }
}
