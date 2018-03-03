using System;

namespace Assets.General.NetworkMessage
{
    enum ImageType
    {
        Map,
        CameraImage
    }

    [Serializable]
    class ChangeTypeImageNetworkMessage : NetworkMessage
    {
        public ImageType imageType;
    }
}
