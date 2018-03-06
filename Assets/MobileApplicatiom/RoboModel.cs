using System;
using Assets.General;
using Assets.General.NetworkMessage;
using UnityEngine;
using System.Collections.Generic;

namespace Assets.MobileApplicatiom
{
    public class RoboModel : MonoBehaviour, IModel
    {
        public event EventHandler<VideoMessageEventArgs> ModelVideoStateChangedEvent;
        public event EventHandler<MapMessageEventArgs> ModelMapStateChangedEvent;

        private Queue<NetworkMessage> messages = new Queue<NetworkMessage>();

        public void Start()
        {
            ConnectionManager.Instanse.SetConnectionOptions(6007, 6006);
            ConnectionManager.Instanse.RecievedMessage += RecieveMessage;
        }

        public void Update()
        {
            ProcessQueue();
        }

        private void RecieveMessage(object sender, MessageEventArgs e)
        {
            messages.Enqueue(NetworkMessage.Deserialize(e.message));
        }

        public void OnDestroy()
        {
            ConnectionManager.Instanse.RecievedMessage -= RecieveMessage;
            ConnectionManager.Instanse.Terminate();
        }

        private void ProcessQueue()
        {
            if (messages.Count > 0)
            {
                var message = messages.Dequeue();

                if (message is VideoNetworkMessage)
                    RecieveVideoMessage(message as VideoNetworkMessage);

                if (message is MapNetworkMessage)
                    RecieveMapMessage(message as MapNetworkMessage);
            }
        }

        private void RecieveVideoMessage(VideoNetworkMessage videoNetworkMessage)
        {
            var texture = new Texture2D(0, 0);

            texture.LoadImage(videoNetworkMessage.texture);
            texture.Apply();

            if (ModelVideoStateChangedEvent != null)
            {
                var eventArgs = new VideoMessageEventArgs();
                eventArgs.message = texture;

                ModelVideoStateChangedEvent(this, eventArgs);
            }
        }

        private void RecieveMapMessage(MapNetworkMessage mapNetworkMessage)
        {
            if (ModelMapStateChangedEvent != null)
            {
                var eventArgs = new MapMessageEventArgs();
                eventArgs.map = mapNetworkMessage.map;

                ModelMapStateChangedEvent(this, eventArgs);
            }
        }

        public void Move(MotionDirection motion, float distance)
        {
            var message = new MotionNetworkMessage(motion, distance);
            ConnectionManager.Instanse.Send(message.Serialize());
        }
    }
}
