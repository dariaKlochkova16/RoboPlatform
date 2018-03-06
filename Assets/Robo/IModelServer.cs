using Assets.General;
using System;
using UnityEngine;

namespace Assets.Robo
{
    interface IModelServer
    {
        event EventHandler<MotionEventArgs> MovementEvent;
        void SetVideoImage(Texture2D texture);
        void SetMap(Map map);
    }
}
