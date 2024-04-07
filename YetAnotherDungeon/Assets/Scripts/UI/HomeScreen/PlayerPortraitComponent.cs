using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UserCreation;

namespace UI.HomeScreen
{
    public class PlayerPortraitComponent : MonoBehaviour
    {
        PlayerData m_data;
        
        [SerializeField] Button m_portraitButton;

        public void SetData(PlayerData data)
        {
            m_data = data;
            m_portraitButton.GetComponentInChildren<TMP_Text>().text = data.Name;
        }

        public void SetButtonCallback(UnityAction callBack)
        {
            if (m_portraitButton == null)
            {
                return;
            }
            
            m_portraitButton.onClick.RemoveAllListeners();
            m_portraitButton.onClick.AddListener(callBack);
        }
    }
}