using UnityEngine;
using UnityEngine.UIElements;

public class TutorialText : MonoBehaviour
{
    public GameObject UIElement; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        TextTutorial();
    }

    void TextTutorial()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D)) 
            {
            UIElement.SetActive(false);
            }
    }
}
