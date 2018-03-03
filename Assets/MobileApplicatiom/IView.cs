using Assets.General;
using System;

namespace Assets.MobileApplicatiom
{
    interface IView
    {
        event EventHandler<UIMessageEventArgs> UserInputEvent;
    }
}
