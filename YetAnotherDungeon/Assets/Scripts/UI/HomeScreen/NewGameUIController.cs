using deVoid.UIFramework;
using deVoid.Utils;
using TMPro;
using UI.Popup;
using UnityEngine;

namespace UI.StartupScene
{
    public class NewGameUIController : AWindowController
    {
        [SerializeField] TMP_InputField m_playerName;


        public void OnCreateNewSave()
        {
            var playerName = m_playerName.text;
            PopupOptionsProperties data =
                new PopupOptionsProperties("InValid user name!!!", "Confirm", "Cancel", () => { }, null);
            Signals.Get<ShowPopupOptionsSignal>().Dispatch(data);
            Debug.Log("InValid player name");
        }
    }
}