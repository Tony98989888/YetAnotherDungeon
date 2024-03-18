using System;
using System.Collections.Generic;
using deVoid.UIFramework;
using UnityEngine;
using UnityEngine.UIElements;
using UserCreation;

namespace UI.HomeScreen
{
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
        [SerializeField]
        PlayerPortraitComponent m_playerPortraitEntry = null;

        List<PlayerPortraitComponent> m_allPlayers = new List<PlayerPortraitComponent>();

        protected override void OnPropertiesSet()
        {
            base.OnPropertiesSet();
            SetData(Properties.PlayerData);
        }

        public void SetData(List<PlayerData> data)
        {
            for (int i = m_allPlayers.Count; i > 0; i--)
            {
                Destroy(m_allPlayers[i].gameObject);
            }
            m_allPlayers.Clear();
            var instance = Instantiate(m_playerPortraitEntry, m_playerPortraitEntry.transform.parent, false);
            instance.gameObject.SetActive(true);
            m_allPlayers.Add(instance);
        }
    }
}