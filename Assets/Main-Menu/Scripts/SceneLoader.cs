using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneLoader : MonoBehaviour
{
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
        SceneManager.LoadScene("Menu2");
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
