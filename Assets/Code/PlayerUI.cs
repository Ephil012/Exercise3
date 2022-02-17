using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    public int deplete = 1;
    bool foxAlive = true;

    public TextMeshProUGUI healthUI;
    public TextMeshProUGUI berryUI;
    public TextMeshProUGUI appleUI;
    public TextMeshProUGUI mushroomUI;
    private void Start()
    {
        healthUI.text = "Child Health: " + PublicVars.health;
        berryUI.text = "Berry Count: " + PublicVars.berryCount;
        appleUI.text = "Apple Count: " + PublicVars.appleCount;
        mushroomUI.text = "Mushroom Count: " + PublicVars.mushroomCount;

    }

    private void onTriggerEnter(Collider other)
    {
        if(other.CompareTag("FoxChild") )
        {
            PublicVars.health++;
            healthUI.text = "Child Health : " + PublicVars.health;
            
        }
    }

    void Update()
    {
        PublicVars.health -= deplete * (int)Time.deltaTime ; 
        if(PublicVars.health <=0){
            foxAlive = false;
        }

        mushroomUI.text = "Mushroom Count: " + PublicVars.mushroomCount;
        berryUI.text = "Berry Count: " + PublicVars.berryCount;
        appleUI.text = "Apple Count: " + PublicVars.appleCount;


        healthUI.text = "Child Health: " + PublicVars.health;
        
        
        
        
    }
}