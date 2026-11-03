using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Setup.Manager
{
    public class SceneManager : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] List<string> scenesToLoad;
        [SerializeField] string startUpScene;
        Dictionary<string, List<GameObject>> sceneRoots = new Dictionary<string, List<GameObject>>();
        string currentScene;

#if UNITY_EDITOR
        [Header("Testing")]
        [SerializeField] string sceneName;
        [SerializeField] bool enableScene = false;
        [SerializeField] bool disableScene = false;

        void Update()
        {
            if (enableScene)
            {
                enableScene = false;
                EnableScene(sceneName);
            }

            if (disableScene)
            {
                disableScene = false;
                DisableScene(sceneName);
            }
        }
#endif

        //-----------------------------------------------------

        void Awake() => StartCoroutine(LoadAllScenesAtStart());

        IEnumerator LoadAllScenesAtStart()
        {
            foreach (string sceneName in scenesToLoad)
            {
                if (!UnityEngine.SceneManagement.SceneManager.GetSceneByName(sceneName).isLoaded)
                {
                    AsyncOperation asyncLoad = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
                    while (!asyncLoad.isDone)
                        yield return null;
                }

                yield return null;

                Scene scene = UnityEngine.SceneManagement.SceneManager.GetSceneByName(sceneName);
                if (scene.IsValid() && scene.isLoaded)
                {
                    GameObject[] roots = scene.GetRootGameObjects();
                    sceneRoots[sceneName] = new List<GameObject>(roots);

                    //Deactivate All At Begining
                    foreach (GameObject root in roots)
                        root.SetActive(false);
                }
            }

            if (startUpScene != "")
                EnableScene(startUpScene);
        }

        //-----------------------------------------------------

        public void EnableScene(string sceneName)
        {
            foreach (string scene in scenesToLoad)
                DisableScene(scene);

            if (sceneRoots.TryGetValue(sceneName, out var roots))
            {
                foreach (var go in roots)
                    go.SetActive(true);
                currentScene = sceneName;
            }
            else
            {
                Debug.LogWarning($"SceneManager: '{sceneName}' could not be found.");
                return;
            }
        }

        public void DisableScene(string sceneName)
        {
            if (sceneRoots.TryGetValue(sceneName, out var roots))
            {
                foreach (var go in roots)
                    go.SetActive(false);
            }
            else
                Debug.LogWarning($"SceneManager: '{sceneName}' could not be found.");
        }

        //-----------------------------------------------------

        public bool IsSceneEnabled(string sceneName)
        {
            if (sceneRoots.TryGetValue(sceneName, out var roots) && roots.Count > 0)
                return roots[0].activeSelf;

            return false;
        }

        public string GetCurrentScene() => currentScene;
    }
}