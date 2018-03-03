using Assets.MobileApplicatiom;
using System;
using UnityEngine;

namespace Assets.General
{
    public class MessageEventArgs : EventArgs
    {
        public byte[] message;
    }

    public class VideoMessageEventArgs : EventArgs
    {
        public Texture2D message;
    }

    public class MapMessageEventArgs : EventArgs
    {
        public Map map;
    }

    public class UIMessageEventArgs : EventArgs
    {
        public UIMessage message;
    }
}
