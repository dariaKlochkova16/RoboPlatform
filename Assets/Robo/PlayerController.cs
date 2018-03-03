using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using Assets.General;
using Assets.General.NetworkMessage;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public Image image;
    public GameObject lidar;
    public RenderTexture texture;
    private LidarController lidarController;
    private Queue<byte[]> messages = new Queue<byte[]>();

    void Start()
    {
        lidarController = lidar.GetComponent<LidarController>();
        ConnectionMenager.SetConnectionOptions(6006, 6007);
        ConnectionMenager.RecievedMessage += new EventHandler<MessageEventArgs>(RecieveMessage);

        CreateMap();
    }

    void Update()
    {
        ProcessQueue();

        SentVideoMessage();
    }

    private void SentVideoMessage()
    {
        Texture2D tex = GetRTPixels(texture);

        VideoNetworkMessage message = new VideoNetworkMessage();
        message.texture = tex.EncodeToPNG();
        var mes = message.Serialize();

        var mes2 = NetworkMessage.Deserialize(mes);

        Debug.Log(tex.GetPixel(30, 30));

        ConnectionMenager.Instanse.Send(mes);
    }

    public Texture2D GetRTPixels(RenderTexture rt)
    {
        RenderTexture currentActiveRT = RenderTexture.active;

        RenderTexture.active = rt;

        Texture2D tex = new Texture2D(rt.width, rt.height);
        tex.ReadPixels(new Rect(0, 0, tex.width, tex.height), 0, 0);

        RenderTexture.active = currentActiveRT;
        return tex;
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        transform.position += movement * speed;
    }

    void RecieveMessage(object sender, EventArgs e)
    {
        MessageEventArgs eventArgs = e as MessageEventArgs;
        messages.Enqueue(eventArgs.message);
    }

    void CreateMap()
    {
        MapCreator mapCreator = GetComponent<MapCreator>();

        float angle = 1.0f;
        mapCreator.CreateMap(lidarController, ref angle);

        float[] map = mapCreator.map;

        byte[] mess = BitConverter.GetBytes(map[0]);

        var mapMessage = new VideoNetworkMessage();
        mapMessage.texture = mess;

        ConnectionMenager.Instanse.Send(mapMessage.Serialize());
    }

    void ProcessQueue()
    {
        if (messages.Count > 0)
        {
            byte[] ms = messages.Peek();
            NetworkMessage message = NetworkMessage.Deserialize(ms);

            if (message is MotionNetworkMessage)
            {
                MotionNetworkMessage motionMessage = message as MotionNetworkMessage;
                Vector3 movement = new Vector3(0.0f, 0.0f, 0.0f);

                switch (motionMessage.MotionType)
                {
                    case MotionType.Right:
                        movement = new Vector3(3.0f, 0.0f, 0.0f);
                        break;
                    case MotionType.Left:
                        movement = new Vector3(-3.0f, 0.0f, 0.0f);
                        break;
                    case MotionType.Forward:
                        movement = new Vector3(0.0f, 0.0f, 3.0f);
                        break;
                    case MotionType.Backward:
                        movement = new Vector3(0.0f, 0.0f, -3.0f);
                        break;
                }
                transform.position += movement;
            }
            messages.Dequeue();
        }
    }
}
