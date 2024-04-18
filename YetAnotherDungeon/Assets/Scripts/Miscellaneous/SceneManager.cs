using UnityEngine;
using System.Collections;

namespace Map.MapSystem
{
    public class SceneManager : Singleton<SceneManager>
    {

        public delegate void SceneLoadedCallback();
        
        public void LoadScene(string sceneName, SceneLoadedCallback cb)
        {
            StartCoroutine(LoadSceneAsync(sceneName, cb));
        }

        public IEnumerator LoadSceneAsync(string sceneName, SceneLoadedCallback cb)
        {
            AsyncOperation asyncLoad = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName);
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
            AsyncOperation asyncUnload = UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(sceneName);
            
            while (!asyncUnload.isDone)
            {
                yield return null;
            }
            
            Debug.Log($"Scene:{sceneName} load process finished!");
        }
    }
}
