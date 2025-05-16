using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;
using UnityEngine.SceneManagement;
public class SceneLoader : MonoBehaviour
{
    private int currentSceneIndex;
    private int sceneToContinue;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Tutorial()
    {
        
        SceneManager.LoadScene("Tutorial");
    }

    public void Menu()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Avoid saving the menu scene
        if (currentSceneIndex != 0)
        {
            PlayerPrefs.SetInt("SavedScene", currentSceneIndex);
            PlayerPrefs.Save();
        }

        SceneManager.LoadScene("Menu2");
    }

    public void ContinueGame()
    {
        if (!PlayerPrefs.HasKey("SavedScene"))
        {
            Debug.LogWarning("No saved scene found.");
            return;
        }

        int sceneToContinue = PlayerPrefs.GetInt("SavedScene");

        if (sceneToContinue > 0)
        {
            Debug.Log("Continuing to scene index: " + sceneToContinue);
            SceneManager.LoadScene(sceneToContinue);
        }
        else
        {
            Debug.LogWarning("Saved scene index is 0 or invalid.");
        }
    }


    public void QuitGame()
    {
        Application.Quit();
    }

    public void Level1()
    {
        SceneManager.LoadScene("Level-1");
    }

    public void Level2()
    {
        SceneManager.LoadScene("Level-2");
    }
    public void Options()
    {
        SceneManager.LoadScene("Options");
    }
    public void LevelSelect()
    {
        SceneManager.LoadScene("LevelSelect");
    }


}
