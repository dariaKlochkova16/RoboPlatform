using Assets.General;
using UnityEngine;

namespace Assets.Robo
{
    class CameraEmulator : MonoBehaviour, ICamera
    {
        public RenderTexture cameraOutput;

        public Texture2D GetVideoImage()
        {
            Texture2D tex = Tools.GetRTPixels(cameraOutput);
            return tex;
        }
    }
}
