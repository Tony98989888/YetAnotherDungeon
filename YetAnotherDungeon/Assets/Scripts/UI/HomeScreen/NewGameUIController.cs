using deVoid.UIFramework;
using deVoid.Utils;
using Map.MapSystem;
using TMPro;
using TMPro.Examples;
using UI.Popup;
using UnityEngine;
using UserCreation;

namespace UI.StartupScene
{
    public class NewGameUIController : AWindowController
    {
        [SerializeField] TMP_InputField m_playerName;

        public void OnCreateNewSave()
        {
            PlayerData playerData = new PlayerData
            {
                Name = m_playerName.text,
                CurrentMap = MapIndex.Startup,
            };

            PopupOptionsProperties data =
                new PopupOptionsProperties("Are you sure?", "Confirm", "Cancel",
                    () => { OnConfirmCreateNewUser(playerData); }, () => { });
            Signals.Get<ShowPopupOptionsSignal>().Dispatch(data);
        }

        void OnConfirmCreateNewUser(PlayerData data)
        {
            var savePath = UserDataUtilities.CreateNewUser();
            if (savePath != null)
            {
                data.SavePath = savePath;
                UserDataUtilities.SavePlayerData(data);
            }

            EventBetter.Raise(new OnBeginNewGame() { PlayerData = data });
            SceneManager.Instance.LoadScene(SceneNames.SampleScene, () => {Debug.Log("Ready"); });
        }
    }
}