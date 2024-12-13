using UnityEngine;

namespace FSM.Scripts
{
    public class FsmExample : MonoBehaviour
    {
        private Animator _animator;
        private Fsm _fsm;

        public delegate void CollisionDetectedHandler(GameObject target);
        public event CollisionDetectedHandler OnCollisionDetected;  // Событие для коллизии

        private void Start()
        {
            _animator = GetComponent<Animator>();

            _fsm = new Fsm(_animator);  // Передаем Animator в Fsm
            _fsm.AddState(new FsmStateIdle(_fsm, _animator));
            _fsm.AddState(new FsmStateSearch(_fsm, _animator));
            _fsm.AddState(new FsmStateCollect(_fsm, _animator));

            _fsm.SetState<FsmStateIdle>();
        }

        private void Update()
        {
            _fsm.Update();
        }

        private void OnTriggerEnter(Collider other)
        {
            // Проверка, что мы столкнулись с целью
            if (other.CompareTag("Target"))
            {
                Debug.Log("Target reached: Switching to Collect State");

                // Вызываем событие при столкновении с целью
                OnCollisionDetected?.Invoke(other.gameObject);

                // Передаем цель в состояние сбора
                var collectState = _fsm.GetState<FsmStateCollect>();
                collectState.SetTarget(other.gameObject);

                _fsm.SetState<FsmStateCollect>(); // Переключаемся на состояние сбора
            }
        }

    }

}
    
