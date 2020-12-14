using UnityEngine;
using UnityEngine.AI;

namespace ARPG.Players
{
    public class NavmeshSampler
    {
        private readonly Camera _camera;

        public bool SampleGroundPosition(Vector3 position, out NavMeshHit navMeshHit)
        {
            return NavMesh.SamplePosition(position, out navMeshHit, 1.0f, NavMesh.AllAreas);
        }
    }
}