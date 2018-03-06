using Assets.General;
using Assets.General.NetworkMessage;
using System;

namespace Assets.MobileApplicatiom
{
    interface IModel
    {
        event EventHandler<VideoMessageEventArgs> ModelVideoStateChangedEvent;
        event EventHandler<MapMessageEventArgs> ModelMapStateChangedEvent;

        void Move(MotionDirection motion, float distance);
    }
}
