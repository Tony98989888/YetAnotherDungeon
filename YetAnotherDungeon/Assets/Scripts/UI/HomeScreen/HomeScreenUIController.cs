using System;
using System.Collections.Generic;
using deVoid.UIFramework;
using deVoid.Utils;
using UI.HomeScreen;
using UI.Popup;
using UnityEngine;
using UserCreation;

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
            Signals.Get<ShowPopupOptionsSignal>().AddListener(OnShowPopupWindow);
            Signals.Get<ShowUserSelectionUISignal>().AddListener(OnShowUserSelectionWindow);
        }

        void OnDestroy()
        {
            Signals.Get<NewGameSignal>().RemoveListener(OnNewGameStart);
            Signals.Get<ShowPopupOptionsSignal>().RemoveListener(OnShowPopupWindow);
            Signals.Get<ShowUserSelectionUISignal>().RemoveListener(OnShowUserSelectionWindow);
        }


        void OnShowUserSelectionWindow()
        {
            var data = UserDataHandler.LoadAllSaves();
            m_uiFrame.OpenWindow(ScreenID.UserSelectionUI, new UserSelectionProperties(data));
        }

        void OnNewGameStart()
        {
            m_uiFrame.OpenWindow(ScreenID.NewGameUI);
        }

        void OnShowPopupWindow(PopupOptionsProperties data)
        {
            m_uiFrame.OpenWindow(ScreenID.OptionsPanelUI, data);
        }


        void Start()
        {
            m_uiFrame.OpenWindow(ScreenID.TitleScreenUI);
        }
    }
}