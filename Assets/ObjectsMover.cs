using UnityEngine;
//all
namespace SampleProject
{
    public sealed class ObjectsMover : MonoBehaviour
    {
        [SerializeField]
        private MovingGroupManager movingGroupManager;

        private MoveAgent[] agents;

        private void Awake()
        {
            this.agents = FindObjectOfType<MoveAgent>();
        }

        private void Update()
        {
            if (!Input.GetMouseButtonDown(1)) 
            {
                return;
            }

            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (!Physics.Raycast(ray, out var hit))
            {
                return;
            }

            if (hit.transform.CompareTag("Ground"))
            {
                this.MoveToPosition(hit.point);
            }

        }

        private void MoveToPosition(Vector3 targetPosition)
        {
            targetPosition.y = 0;

            this.movingGroupManager.AddGroup(this.agents, targetPosition);

            foreach (var agent in this.agents)
            {
                agent.MoveToPosition(targetPosition);
            }
        }

        private Vector3 CalculateCenter(Vector3[] points)
        {
            var result = Vector3.zero;
            foreach(var point in points)
            {
                result += point;
            }

            return result / points.Length;
        }
    }
}

