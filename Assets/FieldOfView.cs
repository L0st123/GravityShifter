using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float m_FieldOfView;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_FieldOfView = 60f;
    }

    // Update is called once per frame
    void Update()
    {
        Camera.main.fieldOfView = m_FieldOfView;
        ChangePOV();
    }

    void ChangePOV()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            m_FieldOfView = 90f;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            m_FieldOfView = 60f;
        }
    }
}
