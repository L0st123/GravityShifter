
using System;
using TMPro;
using UnityEngine;

public class Target : MonoBehaviour
{

    public float health = 100f;
    public TextMeshProUGUI text;
    public int targetsHit; 


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        targetsHit = 0;
        health = 100f; 
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("target"+ health);
        
        text.SetText("Targets Hit: "+ targetsHit );
    }


  
    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            targetsHit = + 1;
            
        }
    }

 
}
