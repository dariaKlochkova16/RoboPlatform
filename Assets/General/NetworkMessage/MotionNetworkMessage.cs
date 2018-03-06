using System;

namespace Assets.General.NetworkMessage
{
    [Serializable]
    public class MotionNetworkMessage : NetworkMessage
    {
        public MotionDirection MotionType;
        public float Distance;

        public MotionNetworkMessage(MotionDirection motionType, float distance)
        {
            MotionType = motionType;
            Distance = distance;
        }
    }
}
