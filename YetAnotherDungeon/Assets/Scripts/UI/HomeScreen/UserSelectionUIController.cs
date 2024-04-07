using System;
using System.Collections.Generic;
using deVoid.UIFramework;
using deVoid.Utils;
using UnityEngine;
using UnityEngine.UI;
using UserCreation;

namespace UI.HomeScreen
{
    public class ShowUserSelectionUISignal : ASignal
    {
    }

    [Serializable]
    public class UserSelectionProperties : WindowProperties
    {
        public readonly List<PlayerData> PlayerData;

        public UserSelectionProperties(List<PlayerData> playerData)
        {
            PlayerData = playerData;
        }
    }

    public class UserSelectionUIController : AWindowController<UserSelectionProperties>
    {
        [SerializeField] PlayerPortraitComponent m_playerPortraitEntry = null;

        List<PlayerPortraitComponent> m_allPlayers = new List<PlayerPortraitComponent>();

        [SerializeField] GridLayoutGroup m_userContainer;

        protected override void OnPropertiesSet()
        {
            base.OnPropertiesSet();
            SetData(Properties.PlayerData);
        }

        public void SetData(List<PlayerData> data)
        {
            for (int i = m_allPlayers.Count - 1; i > 0; i--)
            {
                Destroy(m_allPlayers[i].gameObject);
            }

            m_allPlayers.Clear();

            for (int i = 0; i < data.Count; i++)
            {
                var instance = Instantiate(m_playerPortraitEntry, m_userContainer.transform, false);
                instance.SetData(data[i]);
                instance.SetButtonCallback(() => { Debug.Log("Begin Game"); });
                instance.gameObject.SetActive(true);
                m_allPlayers.Add(instance);
            }
        }
    }
}