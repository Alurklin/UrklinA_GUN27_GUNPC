using System.Collections;
using System.Collections.Generic;
using Ecs;
using UnityEngine;
using UnityEngine.AI;

namespace SampleProject
{
    [RequireComponent(typeof(Entity))]
    public class MoveAgent : MonoBehaviour
    {
        private const float STOPPING_DISTANCE_SQR = 0.2f;
        private const float COMPLETE_DELAY = 0.1f;
        private const float CORRECT_PATH_PERIOD = 0.75f;
        private const float SHIFT_OFFSET = 2.0f;
        private const float SHIFT_FACTOR = 0.75f;

        private Entity unit;
        private Vector3 destination;
        private NavMeshPath navMeshPath;

        private Coroutine moveCoroutine;
        private Coroutine completeCorourine;
        private Coroutine checkObstacleCoroutine;
        private Coroutine avoidObstacleCoroutine;

        private Vector3[] pointPath;
        private int pointer;
        private bool isCompleted;
        private float correctPathTime;

        public bool IsCompleted
        {
            get { return this.isCompleted; }
        }

        public bool IsObstacleAvoid
        {
            get { return this.avoidObstacleCoroutine != null; }
        }
    }
}

