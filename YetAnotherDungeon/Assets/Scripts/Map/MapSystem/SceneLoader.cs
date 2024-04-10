using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

namespace Map.MapSystem
{
    public class SceneLoader : Singleton<SceneLoader>
    {

        public delegate void SceneLoadedCallback();
        
        // 异步加载场景
        public void LoadScene(string sceneName, SceneLoadedCallback cb)
        {
            StartCoroutine(LoadSceneAsync(sceneName, cb));
        }

        public IEnumerator LoadSceneAsync(string sceneName, SceneLoadedCallback cb)
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
            while (!asyncLoad.isDone)
            {
                yield return null;
            }
            
            cb?.Invoke();
            
            Debug.Log($"Scene:{sceneName} load process finished!");
        }
        
        public void UnloadScene(string sceneName)
        {
            StartCoroutine(UnloadSceneAsync(sceneName));
        }

        private IEnumerator UnloadSceneAsync(string sceneName)
        {
            AsyncOperation asyncUnload = SceneManager.UnloadSceneAsync(sceneName);
            
            while (!asyncUnload.isDone)
            {
                yield return null;
            }
            
            Debug.Log($"Scene:{sceneName} load process finished!");
        }
    }
}
