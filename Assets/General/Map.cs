using System;

namespace Assets.General
{
    [Serializable]
    public class Map
    {
        public float[] map;

        public int Length { get { return map.Length; } }

        public Map(int angle)
        {
            map = new float[(int)(360 / angle)];
        }

        public void SetValue(int i, float value)
        {
            if (i > 0 && i < Length)
            {
                map[i] = value;
            }
        }
    }

}
