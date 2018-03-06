using Assets.General;
using UnityEngine;

namespace Assets.Robo
{
    class MapProviderEmulator : MonoBehaviour, IMapProvider
    {
        int angle = 1;

        private float SendRay()
        {
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;
            Physics.SphereCast(ray, 0.75f, out hit);

            return hit.distance;
        }

        private void Rotate()
        {
            Transform transform = GetComponent<Transform>();
            transform.Rotate(0, angle, 0);
        }

        public Map GetMap()
        {
            var map = new Map(angle);

            for (int i = 0; i < map.Length; i++)
            {
                map.SetValue(i,SendRay());
                Rotate();
            }
            return map;
        }
    }
}
