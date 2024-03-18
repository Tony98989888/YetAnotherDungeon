using deVoid.UIFramework;
using UnityEngine;
using UnityEngine.UI;

namespace UI.StartupScene
{
    public class NewGameUIController : AWindowController
    {
        [SerializeField]
        InputField m_playerName;
        
        
        public void OnCreateNewSave()
        {
            var playerName = m_playerName.text;
        }
    }
}