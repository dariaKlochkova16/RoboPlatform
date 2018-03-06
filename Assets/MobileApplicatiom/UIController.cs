using Assets.General;
using Assets.General.NetworkMessage;
using UnityEngine;

namespace Assets.MobileApplicatiom
{
    class UIController : MonoBehaviour
    {
        //private IView view;
        //private IModel model;

        public UIView view;
        public RoboModel model;

        public void Start()
        {
            view.UserInputEvent += ReciveUIViewMessage;
        }

        private void ReciveUIViewMessage(object sender, UIMessageEventArgs e)
        {
            switch(e.message.MessageId)
            {
                case "forward":
                    model.Move(MotionDirection.Backward, 1);
                    break;
                case "backward":
                    model.Move(MotionDirection.Backward, 1);
                    break;
                case "left":
                    model.Move(MotionDirection.Left, 1);
                    break;
                case "right":
                    model.Move(MotionDirection.Right, 1);
                    break;
                default:
                    break;
            }
        }
    }
}
