using Assets.General;
using Assets.General.NetworkMessage;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Robo
{
    class RoboModelServer : MonoBehaviour, IModelServer
    {
        public event EventHandler<MotionEventArgs> MovementEvent;
        private Queue<byte[]> messages = new Queue<byte[]>();

        public void Start()
        {
            ConnectionManager.Instanse.SetConnectionOptions(6006, 6007);//TODO кто это должен делать?
            ConnectionManager.Instanse.RecievedMessage += RecieveMessage;
        }

        public void Update()
        {
            ProcessQueue();
        }

        public void OnDestroy()
        {
            ConnectionManager.Instanse.RecievedMessage -= RecieveMessage;
            ConnectionManager.Instanse.Terminate();
        }

        private void RecieveMessage(object sender, MessageEventArgs e)
        {
            MessageEventArgs eventArgs = e as MessageEventArgs;
            messages.Enqueue(eventArgs.message);
        }

        public void SetVideoImage(Texture2D texture)
        {
            var message = new VideoNetworkMessage();
            message.texture = texture.EncodeToPNG();
            ConnectionManager.Instanse.Send(message.Serialize());
        }

        public void SetMap(Map map)
        {
            var message = new MapNetworkMessage();
            message.map = map;
            ConnectionManager.Instanse.Send(message.Serialize());
        }

        private void ProcessQueue()
        {
            if (messages.Count > 0)
            {
                byte[] ms = messages.Peek();
                NetworkMessage message = NetworkMessage.Deserialize(ms);

                if (message is MotionNetworkMessage)
                {
                    MotionNetworkMessage motionMessage = message as MotionNetworkMessage;
                    var e = new MotionEventArgs();
                    e.motionType = motionMessage.MotionType;
                    e.distance = motionMessage.Distance;

                    MovementEvent(this, e);

                }
                messages.Dequeue();
            }
        }
    }
}
