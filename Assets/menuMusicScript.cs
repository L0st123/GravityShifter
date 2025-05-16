using UnityEngine;
using UnityEngine.SceneManagement;

public class menuMusicScript : MonoBehaviour
{
    private static menuMusicScript instance;

    [SerializeField] string[] scenesToIgnore;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        foreach (string sceneName in scenesToIgnore)
        {
            if (scene.name == sceneName)
            {
                Destroy(gameObject);
                return;
            }
        }
    }
}
