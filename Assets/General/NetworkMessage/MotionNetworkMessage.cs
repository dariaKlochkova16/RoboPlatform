using System;

namespace Assets.General.NetworkMessage
{
    [Serializable]
    public class MotionNetworkMessage : NetworkMessage
    {
        public MotionType MotionType;

        public MotionNetworkMessage(MotionType motionType)
        {
            MotionType = motionType;
        }
    }
}
