using System.Collections;
using System.Collections.Generic;
using Ecs;
using UnityEngine;
using UnityEngine.AI;

//all
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

        public bool CanCorrectPath
        {
            get { return Time.time - this.correctPathTime >= CORRECT_PATH_PERIOD; }
        }

        public bool IsLastPoint
        {
            get { return this.pointer >= this.pointPath.Length - 1; }
        }

        public void Awake()
        {
            this.unit = this.GetComponent<Entity>();
            this.navMeshPath = new NavMeshPath();
        }

        #region Move

        public void MoveToPosition(Vector3 destination)
        {
            this.StopMove(isCompleted: false);
            this.StartMove(destination);
        }

        private void StartMove(Vector3 destination)
        {
            this.destination = destination;

            var pathGenerated = NavMesh.CalculatePath(
                this.transform.position,
                this.destination,
                NavMesh.AllAreas,
                this.navMeshPath
            );

            if (!pathGenerated)
            {
                return;
            }

            this.pointer = 0;
            this.pointPath = this.navMeshPath.corners;

            this.moveCoroutine = this.StartCoroutine(this.MoveRoutine());
            this.checkObstacleCoroutine = this.StartCoroutine(this.CheckObstacleRoutine());
        }

        private IEnumerator MoveRoutine()
        {
            var framePeriod = new WaitForFixedUpdate();

            while (this.pointer < this.pointPath.Length)
            {
                yield return framePeriod;

                if (this.IsObstacleAvoid)
                {
                    continue;
                }

                this.MoveByPath();
            }

            this.StopMove(isCompleted: true);
        }

        private void MoveByPath()
        {
            var currentPosition = this.transform.position;
            var targetPosition = this.pointPath[this.pointer];
            var distanceVector = targetPosition - currentPosition;

            var IsTargetReached = distanceVector.sqrMagnitude <= STOPPING_DISTANCE_SQR;
            if (IsTargetReached)
            {
                this.pointer++;
                return;
            }

            var direction = distanceVector.normalized;
            this.MoveUnit(direction);
        }

        private void MoveUnit(Vector3 direction)
        {
            this.unit.SetData(new MoveStateComponent
            {
                moveRequired = true,
                direction = direction
            });
        }

        #endregion

        #region CorrectPath

        public void CorrectPath()
        {
            if(!this.CanCorrectPath || this.IsLastPoint)
            {
                return;
            }

            this.correctPathTime = Time.time;

            var currentPosition = this.transform.position;
            var targetPosition = this.pointPath[this.pointer];
            var nextPosition = this.pointPath[this.pointer + 1];

            var line = nextPosition - currentPosition;

            var isRight = Algorithms.PointRelativeToVector(currentPosition, nextPosition, targetPosition) > 0;
            var crossVector = isRight ? Vector3.up : Vector3.down;
            var shiftOffset = Vector3.Cross(line.normalized, crossVector) * SHIFT_OFFSET;

            var newPosition = Vector3.Lerp(
                currentPosition + shiftOffset, nextPosition - shiftOffset, SHIFT_FACTOR
            );

            if (NavMesh.SamplePosition(newPosition, out var hit, 2.0f, NavMesh.AllAreas))
            {
                newPosition = hit.position;
            }

            var pathGenerated = NavMesh.CalculatePath(
                newPosition,
                this.destination,
                NavMesh.AllAreas,
                this.navMeshPath
            );

            if (!pathGenerated)
            {
                this.pointPath[this.pointer] = newPosition;
                return;
            }

            this.pointer = 0;
            this.pointPath = this.navMeshPath.corners;
        }

        public bool TryGetNextPosition(out Vector3 targetPosition)
        {
            var lastPoint = this.pointer >= this.pointPath.Length - 1;
            if (lastPoint)
            {
                targetPosition = default;
                return false;
            }

            targetPosition = this.pointPath[this.pointer];
            return true;
        }

        #endregion

        #region ObstacleAvoidance

        public void StartAvoidObstacle()
        {
            if(this.avoidObstacleCoroutine == null)
            {
                var avoidDirection = Vector3.Cross(this.transform.forward, Vector3.up);
                this.avoidObstacleCoroutine = this.StartCoroutine(this.AvoidObstacleRoutine(avoidDirection));
            }
        }

        private void StartAvoidObstacle(Vector3 avoidDirection)
        {
            if (this.avoidObstacleCoroutine == null)
            {
                this.avoidObstacleCoroutine = this.StartCoroutine(this.AvoidObstacleRoutine(avoidDirection));
            }
        }

        private void StopAvoidObstalce()
        {
            if (this.avoidObstacleCoroutine != null)
            {
                this.StopCoroutine(this.avoidObstacleCoroutine);
                this.avoidObstacleCoroutine = null;
            }
        }

        private IEnumerator AvoidObstacleRoutine(Vector3 moveDirection)
        {
            while (true)
            {
                yield return new WaitForFixedUpdate();
                this.unit.SetData(new MoveStateComponent
                {
                    moveRequired = true,
                    direction = moveDirection
                });
            }
        }

        private IEnumerator CheckObstacleRoutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(0.35f);

                var currentPosition = this.transform.position;
                var targetPosition = this.pointPath[this.pointer];
                var direction = (targetPosition - currentPosition).normalized;

                var ray = new Ray(currentPosition, direction);
                if(!Physics.Raycast(ray, out var hit, 0.35f, LayerMask.GetMask("Obstacle")))
                {
                    this.StopAvoidObstalce();
                }
                else
                {
                    var avoidDirection = Vector3.Cross(hit.normal, Vector3.up);
                    this.StartAvoidObstalce(avoidDirection);
                }
            }
        }

        #endregion

        #region Stop

        public void CompleteMove()
        {
            if(this.IsCompleted || this.completeCorourine != null)
            {
                return;
            }

            this.completeCorourine = this.StartCoroutine(this.CompleteDelayed());
        }

        private IEnumerator CompleteDelayed()
        {
            yield return new WaitForSeconds(COMPLETE_DELAY);
            this.StopMove(isCompleted: true);
        }

        private void StopMove(bool isCompleted)
        {
            if (this.moveCoroutine != null)
            {
                this.StopCoroutine(this.moveCoroutine);
                this.moveCoroutine = null;
            }

            if (this.checkObstacleCoroutine != null)
            {
                this.StopCoroutine(this.checkObstacleCoroutine);
                this.checkObstacleCoroutine = null;
            }

            if (this.completeCorourine != null)
            {
                this.StopCoroutine(this.completeCorourine);
                this.completeCorourine = null;
            }

            if (this.avoidObstacleCoroutine != null)
            {
                this.StopCoroutine(this.avoidObstacleCoroutine);
                this.avoidObstacleCoroutine = null;
            }

            this.isCompleted = isCompleted;
        }

        #endregion

        #region Editor

#if UNITY_EDITOR

        private void OnDrawGizmos()
        {
            try
            {
                this.DrawMovingPath();
            }
            catch (Exception)
            {

            }
        }

        private void DrawMovingPath()
        {
            Gizmos.color = Color.magenta;

            var current = this.transform.position;
            for (int i = this.pointer; i < this.pointPath.Length; i++)
            {
                Gizmos.DrawLine(current, this.pointPath[i]);
                current = this.pointPath[i];
            }
        }
#endif

        #endregion
    }
}