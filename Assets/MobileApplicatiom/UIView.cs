using Assets.General;
using Assets.MobileApplicatiom;
using System;
using UnityEngine;
using UnityEngine.UI;

public class UIView : MonoBehaviour, IView
{
    public event EventHandler<UIMessageEventArgs> UserInputEvent;
    private IModel model;
    public Image image;
    public int scale = 20;

    public void Start()
    {
        model.ModelVideoStateChangedEvent += DrawVideoImage;
        model.ModelMapStateChangedEvent += DrawMapImage;
    }

    private void DrawMapImage(object sender, MapMessageEventArgs e)
    {
        CreateMapTexture(e.map);
    }

    private void ButtonClick(string buttonID)
    {
        UIMessageEventArgs eventArgs = new UIMessageEventArgs();
        eventArgs.message = new UIMessage(buttonID);

        UserInputEvent(this, eventArgs);
    }

    public void DrawVideoImage(object sender, VideoMessageEventArgs e)
    {
        var texture = e.message;
        var rectangular = new Rect(0.0f, 0.0f, texture.width, texture.height);
        var pivot = new Vector2(0.5f, 0.5f);

        Sprite mySprite = Sprite.Create(texture, rectangular, pivot);
        image.sprite = mySprite;
    }

    public Texture2D CreateMapTexture(Map _map)
    {
        //TODO 
        int width = (int)image.flexibleWidth;
        int hight = (int)image.flexibleHeight;
        float Y;
        float X;

        var map = _map.map;

        var texture = new Texture2D(width, hight);

        Y = hight / 2;
        X = width / 2;

        float angle = 360 / map.Length;

        texture.SetPixel((int)X, (int)Y, Color.red);

        for (int i = 0; i < map.Length; i++)
        {
            float x = map[i] * scale * Mathf.Cos(i * angle);
            float y = map[i] * scale * Mathf.Sin(i * angle);

            int x1 = (int)(X + x);
            int y1 = (int)(Y + y);
            texture.SetPixel(x1, y1, Color.black);
        }
        texture.Apply();

        return texture;
    }
}
