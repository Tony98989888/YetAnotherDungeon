using System;
using deVoid.UIFramework;
using deVoid.Utils;
using UnityEngine;

namespace UI.StartupScene
{
    // Main controller for all home screen UI
    public class HomeScreenUIController : MonoBehaviour
    {
        [SerializeField] UISettings m_uiSettings;
        [SerializeField] Camera m_cam;
        [SerializeField] Transform m_eyeTransform;

        UIFrame m_uiFrame;

        void Awake()
        {
            m_uiFrame = m_uiSettings.CreateUIInstance();
            Signals.Get<NewGameSignal>().AddListener(OnNewGameStart);
        }

        void OnNewGameStart()
        {
            m_uiFrame.OpenWindow(ScreenID.NewGameUI);
        }

        private void OnDestroy()
        {
            Signals.Get<NewGameSignal>().RemoveListener(OnNewGameStart);
        }

        void Start()
        {
            m_uiFrame.OpenWindow(ScreenID.TitleScreenUI);
        }
    }
}