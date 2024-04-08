using System;
using System.IO;
using UnityEngine;

namespace UserCreation
{
    [CreateAssetMenu(fileName = "GeneralSettings", menuName = "Settings/GeneralSettings")]
    public class GeneralSettings : GameSettings
    {
        #region General Settings

        private const string SaveFolderName = "Save";

        #endregion


        #region Singleton Access

        private static GeneralSettings _instance;
        private static System.Action<GeneralSettings> _onBeforeInitializeCallback;

        public static bool Initialized
        {
            get { return _instance != null; }
        }

        public static GeneralSettings Settings
        {
            get { return _instance; }
        }

        void OnEnable()
        {
            throw new NotImplementedException();
        }

        public static void Init(System.Action<GeneralSettings> onBeforeInitializeCallback = null)
        {
            _onBeforeInitializeCallback = onBeforeInitializeCallback;
            _instance = GameSettings.GetGameSettings<GeneralSettings>();
        }

        #endregion

        #region Constructor

        protected override void OnInitialized()
        {
            _instance = this;
            if (_onBeforeInitializeCallback != null)
            {
                var d = _onBeforeInitializeCallback;
                _onBeforeInitializeCallback = null;
                d(this);
            }

            //################
            // Initialize Services - this should always happen first, any given service shouldn't rely on accessing other services on creation
            //########
            {
            }
        }

        #endregion

        #region Static Methods

        public static void QuitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        #endregion
    }
}