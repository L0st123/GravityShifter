using UnityEngine;
using UnityEngine.SceneManagement;
public class AnyKeyPress : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        AnyKey();
    }

    void AnyKey()
    {
        if (Input.anyKey)
        {
            SceneManager.UnloadScene("Menu");
            SceneManager.LoadScene("Menu2");
        }
    }
}

