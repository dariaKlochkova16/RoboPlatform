using System.Runtime.Serialization.Formatters.Binary;
using System;
using System.IO;

namespace Assets.General.NetworkMessage
{
    [Serializable]
    public abstract class NetworkMessage
    {
        public byte[] Serialize()
        {
            MemoryStream memoryStream = new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();

            formatter.Serialize(memoryStream, this);

            return memoryStream.ToArray();
        }

        public static NetworkMessage Deserialize(byte[] binaryMessage)
        {
            var memoryStream = new MemoryStream(binaryMessage);
            BinaryFormatter formatter = new BinaryFormatter();

            return (NetworkMessage)formatter.Deserialize(memoryStream);
        }
    }
}





