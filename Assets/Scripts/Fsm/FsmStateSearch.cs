using UnityEngine;
using FSM.Scripts;
using System.Linq;
using UnityEngine.AI;
using DG.Tweening;

public class FsmStateSearch : FsmState
{
    private Animator _animator;

    private int _currentTargetIndex;
    private Transform[] _targets;
    private NavMeshAgent _agent;
    private Transform _characterTransform;  // Хранение ссылки на трансформ персонажа
    private SkinnedMeshRenderer _characterRenderer;  // Ссылка на Renderer для изменения цвета
    private GameObject _smokePrefab;  // Префаб дыма
    private GameObject _smokeInstance;  // Экземпляр дыма

    public FsmStateSearch(Fsm fsm, Animator animator) : base(fsm)
    {
        _animator = animator;  // Сохраняем Animator
        _agent = GameObject.FindGameObjectWithTag("Agent").GetComponent<NavMeshAgent>();  // Получаем компонент NavMeshAgent
        _characterTransform = GameObject.FindGameObjectWithTag("Agent").transform;  // Получаем трансформ персонажа
        _characterRenderer = _characterTransform.GetComponentInChildren<SkinnedMeshRenderer>();  // Получаем компонент Renderer
    }

    public override void Enter()
    {
        // Initialize targets and agent in your game context
        _targets = GameObject.FindGameObjectsWithTag("Target").Select(obj => obj.transform).ToArray();
        _currentTargetIndex = 0;

        if (_targets.Length != 0)
        {
            Debug.Log("Entering Search State: Playing Walk Animation");
            _animator.SetTrigger("Search");
        }
        else 
        {
            Debug.Log("No Targets");
        }
            

        // Подписываемся на событие коллизии
        var fsmExample = GameObject.FindGameObjectWithTag("Agent").GetComponent<FsmExample>();
        fsmExample.OnCollisionDetected += HandleCollision;  // Подписка на событие

        // Изменяем размер персонажа с помощью DOTween
        _characterTransform.DOScale(Vector3.one * 2f, 1f)  // Увеличиваем размер до 1.5 за 1 секунду
            .SetEase(Ease.InOutQuad);  // Выбор плавной анимации

        // Изменяем цвет на красный при входе в состояние поиска
        if (_characterRenderer != null)
        {
            _characterRenderer.material.DOColor(Color.red, 1f);  // Меняем цвет на красный за 1 секунду
        }

        // Загрузка префаба дыма из ресурсов
        _smokePrefab = Resources.Load<GameObject>("SmokePrefab");  // Замените на путь к вашему префабу в папке Resources

        // Создание экземпляра дыма
        if (_smokePrefab != null)
        {
            _smokeInstance = Object.Instantiate(_smokePrefab, _characterTransform.position, Quaternion.identity);
            _smokeInstance.transform.SetParent(_characterTransform);  // Сделаем дым дочерним объектом персонажа
            _smokeInstance.transform.localPosition = new Vector3(0, 0, -1);  // Cдвиг дыма
        }
        else
        {
            Debug.LogError("Smoke prefab not found in Resources folder!");
        }
    }

    public override void Update()
    {
        if (_targets.Length == 0) return;

        var target = _targets[_currentTargetIndex];

        // Проверяем, что агент не в пути и назначаем новую цель
        if (!_agent.pathPending && (_agent.destination != target.position))
        {
            _agent.SetDestination(target.position);  // Устанавливаем цель для агента
        }

        // Проверяем, достиг ли агент цели
        if (Vector3.Distance(_agent.transform.position, target.position) < 0.1f)
        {
            Debug.Log($"Target {_currentTargetIndex} reached.");

            // Переход к следующей цели
            _currentTargetIndex++;

            // Если это была последняя цель, возвращаемся к первой или завершаем
            if (_currentTargetIndex >= _targets.Length)
            {
                Debug.Log("All targets reached.");
                _currentTargetIndex = 0;  // Начинаем снова с первой цели

                // Если необходимо, переключаем состояние, например:
                Fsm.SetState<FsmStateIdle>();
            }

            // Если дым существует, обновляем его позицию, чтобы он следовал за персонажем
            if (_smokeInstance != null)
            {
                _smokeInstance.transform.position = _characterTransform.position + new Vector3(0, 0, -1);
            }
        }

    }

    public override void Exit()
    {
        // Отписываемся от события, когда выходим из состояния
        var fsmExample = GameObject.FindGameObjectWithTag("Agent").GetComponent<FsmExample>();
        fsmExample.OnCollisionDetected -= HandleCollision;

        // Возвращаем размер персонажа к нормальному значению с помощью DOTween
        _characterTransform.DOScale(Vector3.one, 1f)  // Возвращаем к исходному размеру за 1 секунду
            .SetEase(Ease.InOutQuad);  // Плавный переход

        // Изменяем цвет на красный при входе в состояние поиска
        if (_characterRenderer != null)
        {
            _characterRenderer.material.DOColor(Color.white, 1f);  // Меняем цвет на красный за 1 секунду
        }

        // Остановим систему частиц (дым)
        if (_smokeInstance != null)
        {
            var smokeParticleSystem = _smokeInstance.GetComponent<ParticleSystem>();
            if (smokeParticleSystem != null)
            {
                smokeParticleSystem.Stop();  // Остановим дым
            }

            // Удаляем экземпляр дыма
            Object.Destroy(_smokeInstance);
        }
    }

    private void HandleCollision(GameObject target)
    {
        Debug.Log("Handling collision with target: " + target.name);

    }

}
