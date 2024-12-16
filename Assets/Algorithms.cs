using UnityEngine;
//all
namespace SampleProject
{
    public class Algorithms
    {
        public static float PointRelativeToVector(Vector3 start, Vector3 end, Vector3 point)
        {
            return (point.x - start.x) * (end.z - start.z) -
                   (point.z - start.z) * (end.x - start.x);
        }
    }
}

