using UnityEngine;

public class FieldOfView : MonoBehaviour
{
   
    public float targetFOV = 60f;
    public float smoothSpeed = 5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    

    // Update is called once per frame
    void Update()
    {
        if (Camera.main != null)
        {
            
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, targetFOV, Time.deltaTime * smoothSpeed);
        }
        ChangePOV();
    }

    void ChangePOV()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            targetFOV = 90f;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            targetFOV = 60f;
        }
    }
}
