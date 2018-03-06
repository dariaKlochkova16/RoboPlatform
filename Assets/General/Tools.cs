using System;
using UnityEngine;

namespace Assets.General
{
    public static class Tools
    {
        public static Texture2D GetRTPixels(RenderTexture rt)
        {
            RenderTexture currentActiveRT = RenderTexture.active;
            RenderTexture.active = rt;

            Texture2D tex = new Texture2D(rt.width, rt.height);
            tex.ReadPixels(new Rect(0, 0, tex.width, tex.height), 0, 0);

            RenderTexture.active = currentActiveRT;
            return tex;
        }

        internal static Texture2D GetRTPixels(object texture)
        {
            throw new NotImplementedException();
        }
    }
}
