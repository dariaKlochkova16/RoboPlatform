using Assets.General;
using UnityEngine;

namespace Assets.Robo
{
    class MotionControllerEmulator : MonoBehaviour, IMotionController
    {
        public void Move(MotionDirection motionType, float distance = 3.0f)
        {
            var transform = GetComponent<Transform>();
            Vector3 movement = new Vector3(0.0f, 0.0f, 0.0f);

            switch (motionType)
            {
                case MotionDirection.Right:
                    movement = new Vector3(distance, 0.0f, 0.0f);
                    break;
                case MotionDirection.Left:
                    movement = new Vector3(-distance, 0.0f, 0.0f);
                    break;
                case MotionDirection.Forward:
                    movement = new Vector3(0.0f, 0.0f, distance);
                    break;
                case MotionDirection.Backward:
                    movement = new Vector3(0.0f, 0.0f, -distance);
                    break;
            }
            transform.position += movement;
        }
    }
}
