using UnityEngine;

namespace _Project.Develop.Runtime.Utils.RandomManagement
{
    public class RandomService : IRandomService
    {
        public int Range(int min, int max)
        {
            return Random.Range(min, max);
        }

        public float Range(float min, float max)
        {
            return Random.Range(min, max);
        }
    }
}