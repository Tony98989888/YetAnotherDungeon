using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UserCreation;

namespace UI.HomeScreen
{
    public class PlayerPortraitComponent : MonoBehaviour
    {
        [SerializeField] Button m_portraitButton;

        public void SetUserData(PlayerData data)
        {
            m_portraitButton.GetComponentInChildren<TMP_Text>().text = data.Name;
        }
    }
}