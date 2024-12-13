using UnityEngine;

namespace FSM.Scripts
{
    public class FsmExample : MonoBehaviour
    {
        private Animator _animator;
        private Fsm _fsm;

        public delegate void CollisionDetectedHandler(GameObject target);
        public event CollisionDetectedHandler OnCollisionDetected;  // ������� ��� ��������

        private void Start()
        {
            _animator = GetComponent<Animator>();

            _fsm = new Fsm(_animator);  // �������� Animator � Fsm
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
            // ��������, ��� �� ����������� � �����
            if (other.CompareTag("Target"))
            {
                Debug.Log("Target reached: Switching to Collect State");

                // �������� ������� ��� ������������ � �����
                OnCollisionDetected?.Invoke(other.gameObject);

                // �������� ���� � ��������� �����
                var collectState = _fsm.GetState<FsmStateCollect>();
                collectState.SetTarget(other.gameObject);

                _fsm.SetState<FsmStateCollect>(); // ������������� �� ��������� �����
            }
        }

    }

}
    
