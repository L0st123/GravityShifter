using UnityEngine;
using UnityEngine.SceneManagement;

public class door3 : MonoBehaviour
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

        if (other.gameObject.tag == "Player" && Points.points == 60f)
        {
            Debug.Log("");
            SceneManager.LoadScene("Menu2");
        }
    }
}
