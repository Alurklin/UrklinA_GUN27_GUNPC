using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

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

        }
    }
}

