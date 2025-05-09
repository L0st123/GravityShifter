using UnityEngine;
using TMPro;
public class Points : MonoBehaviour
{
   
    public TextMeshProUGUI text;
    public static float points = 0f;
    private void Start()
    {
        points = 0f;

        text.SetText("Points:  " + points);
    }
    private void Update()
    {

        text.SetText("Points:  " + points);

    }

    void PointCollector()
    {

    }



}
