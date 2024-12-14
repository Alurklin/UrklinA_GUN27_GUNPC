using System.Collections.Generic;
using UnityEngine;
//30:19
namespace SampleProject
{
    public sealed class MovingGroupAgent : MonoBehaviour
    {
        private const float STOPPING_DISTANCE = 1.0f;
        private const float OBSTACLE_AVOID_DISTANCE = 2.5f;
        private const float EQUALS_POINT_DISTANCE = 0.1f;
        private const float COMPLETE_RADIUS = 4.0f;
        private const float COMPLETE_RADIUS_COEF = 4 * Mathf.PI;

        private readonly List<MoveAgent> movingAgents;
        private readonly List<MoveAgent> completeAgents;
        private readonly List<MoveAgent> cache;
        private readonly Vector3 destination;
        private readonly float completeRadius;

        public MovingGroupAgent(IEnumerable<MoveAgent> agents, Vector3 destination)
        {
            this.movingAgents = new List<MoveAgent>(agents);
            this.completeAgents = new List<MoveAgent>();
            this.cache = new List<MoveAgent>();

            this.destination = destination;
            this.completeRadius = Mathf.Max(COMPLETE_RADIUS, this.movingAgents.Count / COMPLETE_RADIUS_COEF);
        }

        public bool IsCompleted()
        {
            return this.movingAgents.Count <= 0;
        }

        public void Update()
        {
            this.cache.Clear();
            this.cache.AddRange(this.movingAgents);

            CorrectAgentPaths(this.cache);
            AvoidObstacles(this.cache);

            this.CompleteAgents();
        }

        public void RemoveAgents(IEnmerable<MoveAgent> agents)
        {
            foreach (var agent in agents)
            {
                this.completeAgents.Remove(agent);
                this.movingAgents.Remove(agent);
            }
        }

        private void CompleteAgents()
        {
            this.cache.Clear();
            this.cache.AddRange(this.movingAgents);

            for (int i = 0, count = this.cache.Count; i < count; i++)
            {
                var agent = this.cache[i];
                if (agent.IsCompleted)
                {
                    this.movingAgents.Remove(agent);
                    this.completeAgents.Add(agent);
                }
            }

            for (int i = 0, movingCount = this.movingAgents.Count; i < movingCount; i++)
            {
                var agent = this.movingAgents[i];
                var agentPosition = agent.transform.position;

                if (Vector3.Distance(agentPosition, this.destination) > this.completeRadius)
                {
                    continue;
                }

                if (Vector3.Distance(agentPosition, this.destination) <= STOPPING_DISTANCE)
                {
                    agent.CompleteMove();
                    continue;
                }

                for (int j = 0, completeCount = this.completeAgents.Count; j < completeCount; j++)
                {
                    var otherAgent = this.completeAgents[j];

                    var otherPosition = otherAgent.transform.position;
                    if (Vector3.Distance(agentPosition, otherPosition) < STOPPING_DISTANCE)
                    {
                        agent.CompleteMove();
                        break;
                    }
                }
            }
        }

        #region CorrectPaths

        private static void CorrectAgentPaths(List<MoveAgent> agents)
        {
            var count = agents.Count;

            for (var i = 0; i < count; i++)
            {
                var agent = agents[i];
                CorrectAgentPaths(agent, agents);
            }
        }

        private static void CorrectAgentPath(MoveAgent agent, List<MoveAgent> otherAgents)
        {
            if (!agent.CanCorrectPath)
            {
                return;
            }

            if (HasCorrectedAgentNear(agent, otherAgents))
            {
                Debug.Log("CORRECT NEAR AGENT >>>");
                agent.CorrectPath();
                return;
            }

            if (CheckCollisionForCorrect(agent, otherAgents))
            {
                Debug.Log("CORRECT PATH AGENT >>>");
                agent.CorrectPath();
            }
        }
    }
}