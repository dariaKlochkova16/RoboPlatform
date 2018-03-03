using System;
using Assets.General;
using Assets.General.NetworkMessage;
using UnityEngine;
using System.Collections.Generic;

namespace Assets.MobileApplicatiom
{
    class RoboModel : MonoBehaviour, IModel
    {
        public event EventHandler<VideoMessageEventArgs> ModelVideoStateChangedEvent;
        public event EventHandler<MapMessageEventArgs> ModelMapStateChangedEvent;

        private Queue<NetworkMessage> messages = new Queue<NetworkMessage>();

        public void Start()
        {
            ConnectionMenager.RecievedMessage += RecieveMessage;
        }

        public void Update()
        {
            ProcessQueue();
        }

        private void RecieveMessage(object sender, MessageEventArgs e)
        {
            messages.Enqueue(NetworkMessage.Deserialize(e.message));
        }

        private void ProcessQueue()
        {
            var message = messages.Dequeue();

            if (message is VideoNetworkMessage)
                RecieveVideoMessage(message as VideoNetworkMessage);

            if (message is MapNetworkMessage)
                RecieveMapMessage(message as MapNetworkMessage);
        }

        private void RecieveVideoMessage(VideoNetworkMessage videoNetworkMessage)
        {
            //TODO
            var texture = new Texture2D(0, 0);

            texture.LoadImage(videoNetworkMessage.texture);
            texture.Apply();

            var eventArgs = new VideoMessageEventArgs();
            eventArgs.message = texture;

            ModelVideoStateChangedEvent(this, eventArgs);
        }

        private void RecieveMapMessage(MapNetworkMessage mapNetworkMessage)
        {
            var eventArgs = new MapMessageEventArgs();
            eventArgs.map = mapNetworkMessage.map;

            ModelMapStateChangedEvent(this, eventArgs);
        }

        public void Move(MotionType motion, float distance)
        {
            //TODO distance
            var message = new MotionNetworkMessage(motion);
            ConnectionMenager.Instanse.Send(message.Serialize());
        }

        public void SetCurrentImageType(ImageType imageType)
        {
            var message = new ChangeTypeImageNetworkMessage();
            message.imageType = imageType;

            ConnectionMenager.Instanse.Send(message.Serialize());
        }
    }
}
