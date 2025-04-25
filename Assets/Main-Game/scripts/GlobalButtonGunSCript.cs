using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GlobalButtonGunSCript : MonoBehaviour
{
    public Button button1; 
    public Button button2;  
    public Button button3;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
   

        button1.interactable = true;  
        button2.interactable = true;
        button3.interactable = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            button1.interactable = false;
            button2.interactable = true;
            button3.interactable = true;
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            button1.interactable = true;
            button2.interactable = false;
            button3.interactable = true;
            
        }
        if(Input.GetKey(KeyCode.Alpha3))
        {
            button1.interactable = true;
            button2.interactable = true;
            button3.interactable = false;
        }

    }
}
