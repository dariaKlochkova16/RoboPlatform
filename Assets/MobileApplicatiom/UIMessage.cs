using System;
using Assets.General;

namespace Assets.MobileApplicatiom
{
    public class UIMessage
    {
        public string MessageId { get; set; }

        public UIMessage(string messageId)
        {
            MessageId = messageId;
        }
    }
}
