using System;
using deVoid.UIFramework;
using deVoid.Utils;
using TMPro;
using UnityEngine;

namespace UI.Popup
{
    public class ShowPopupOptionsSignal : ASignal<PopupOptionsProperties>
    {
    }

    [Serializable]
    public class PopupOptionsUIController : AWindowController<PopupOptionsProperties>
    {
        [SerializeField] TextMeshProUGUI m_message;

        [SerializeField] TextMeshProUGUI m_confirmBtnLabel;

        [SerializeField] TextMeshProUGUI m_cancelBtnLabel;

        [SerializeField] GameObject m_cancelBtn;

        protected override void OnPropertiesSet()
        {
            base.OnPropertiesSet();
            m_message.text = Properties.Message;
            m_confirmBtnLabel.text = Properties.ConfirmButtonText;
            m_cancelBtnLabel.text = Properties.CancelButtonText;

            m_cancelBtn.SetActive(Properties.CancelAction != null);
        }

        public void OnConfirm()
        {
            UI_Close();
            if (Properties.ConfirmAction != null)
            {
                Properties.ConfirmAction();
            }
        }

        public void OnCancel()
        {
            UI_Close();
            if (Properties.CancelAction != null)
            {
                Properties.CancelAction();
            }
        }
    }
}