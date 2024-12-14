using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using SampleProject;
//all
namespace SampleProject
{
    public sealed class MovingGroupManager : MonoBehaviour
    {
        private readonly List<MovingGroupAgent> groupAgents = new();
        private readonly List<MovingGroupAgent> cache = new();

        public void AddGroup(IEnumerable<MoveAgent> agents, Vector3 destination)
        {
            foreach (var activeGroup in this.groupAgents.ToList())
            {
                activeGroup.RemoveAgents(agents);
                if (activeGroup.IsCompleted())
                {
                    this.groupAgents.Remove(activeGroup);
                }
            }

            this.groupAgents.Add(new MovingGroupAgent(agents, destination));
        }

        private void FixedUpdate()
        {
            this.cache.Clear();
            this.cache.AddRange(this.groupAgents);

            for (i = 0, count = this.cache.Count; i < count; i++)
            {
                var group = this.cache[i];
                group.Update();

                if (group.IsCompleted())
                {
                    this.groupAgents.Remove(group);
                }
            }
        }
    }
}
