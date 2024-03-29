using System;
using deVoid.UIFramework;
using UnityEngine;

namespace UI.Popup
{
    [Serializable]
    public class PopupOptionsProperties : WindowProperties
    {
        public readonly string Message;
        public readonly string ConfirmButtonText;
        public readonly string CancelButtonText;
        public readonly Action ConfirmAction;
        public readonly Action CancelAction;

        public PopupOptionsProperties(string message, string confirmButtonText, string cancelButtonText,
            Action confirmAction,
            Action cancelAction)
        {
            Message = message;
            ConfirmButtonText = confirmButtonText;
            CancelButtonText = cancelButtonText;
            ConfirmAction = confirmAction;
            CancelAction = cancelAction;
        }
    }
}