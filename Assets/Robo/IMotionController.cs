using Assets.General;

namespace Assets.Robo
{
    interface IMotionController
    {
        void Move(MotionDirection motionType, float distance);
    }
}
