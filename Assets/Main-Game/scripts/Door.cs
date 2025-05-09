using UnityEngine;
using UnityEngine.SceneManagement; 

public class Door : MonoBehaviour
{
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        
     if (other.gameObject.tag =="Player" && Points.points == 30f)
        {
            Debug.Log("if your reading this you are gay");
            SceneManager.LoadScene("Level-1");
        }
    }
    
      
        
    
}
