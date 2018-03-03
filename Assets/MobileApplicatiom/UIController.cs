using Assets.General;
using Assets.General.NetworkMessage;
using UnityEngine;

namespace Assets.MobileApplicatiom
{
    class UIController : MonoBehaviour
    {
        private IView view;
        private IModel model;

        public void Start()
        {
            view.UserInputEvent += ReciveUIViewMessage;
        }

        private void ReciveUIViewMessage(object sender, UIMessageEventArgs e)
        {
            switch(e.message.MessageId)
            {
                case "forward":
                    model.Move(MotionType.Backward, 1);
                    break;
                case "backward":
                    model.Move(MotionType.Backward, 1);
                    break;
                case "left":
                    model.Move(MotionType.Left, 1);
                    break;
                case "right":
                    model.Move(MotionType.Right, 1);
                    break;
                case "lidar":
                    model.SetCurrentImageType(ImageType.Map);
                    break;
                case "camera":
                    model.SetCurrentImageType(ImageType.CameraImage);
                    break;
                default:
                    break;
            }
        }
    }
}
