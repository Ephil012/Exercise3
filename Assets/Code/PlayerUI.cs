using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    public int deplete = 1;
    bool foxAlive = true;

    private float timer = 0f;
    private float interval = 10f;

    public TextMeshProUGUI healthUI;
    public TextMeshProUGUI berryUI;
    public TextMeshProUGUI appleUI;
    public TextMeshProUGUI mushroomUI;
    public TextMeshProUGUI dayUI;
    public TextMeshProUGUI wonUI;
    public TextMeshProUGUI lostUI;
    public GameObject restartButton;

    private void Start()
    {
        healthUI.text = "Child Health: " + PublicVars.health;
        berryUI.text = "Berry Count: " + PublicVars.berryCount;
        appleUI.text = "Apple Count: " + PublicVars.appleCount;
        mushroomUI.text = "Mushroom Count: " + PublicVars.mushroomCount;
        dayUI.text = "Day: " + PublicVars.dayCount;
        restartButton.SetActive(false);
        wonUI.enabled = false;
        lostUI.enabled = false;
    }

    private void setFood() {
        var remainingHealth = 100 - PublicVars.health;

        var mushroomsNeeded = (int) remainingHealth / PublicVars.mushroomHealth;
        if (mushroomsNeeded <= PublicVars.mushroomCount) {
            PublicVars.mushroomCount -= mushroomsNeeded;
            PublicVars.health += mushroomsNeeded * PublicVars.mushroomHealth;
            remainingHealth = 100 - PublicVars.health;
        } else {
            PublicVars.health += PublicVars.mushroomCount * PublicVars.mushroomHealth;
            PublicVars.mushroomCount = 0;
            remainingHealth = 100 - PublicVars.health;
        }

        var applesNeeded = (int) remainingHealth / PublicVars.appleHealth;
        if (applesNeeded <= PublicVars.appleCount) {
            PublicVars.appleCount -= applesNeeded;
            PublicVars.health += applesNeeded * PublicVars.appleHealth;
            remainingHealth = 100 - PublicVars.health;
        } else {
            PublicVars.health += PublicVars.appleCount * PublicVars.appleHealth;
            PublicVars.appleCount = 0;
            remainingHealth = 100 - PublicVars.health;
        }

        var berriesNeeded = (int) remainingHealth / PublicVars.berryHealth;
        if (berriesNeeded <= PublicVars.berryCount) {
            PublicVars.berryCount -= berriesNeeded;
            PublicVars.health += berriesNeeded * PublicVars.berryHealth;
            remainingHealth = 100 - PublicVars.health;
        } else {
            PublicVars.health += PublicVars.berryCount * PublicVars.berryHealth;
            PublicVars.berryCount = 0;
            remainingHealth = 100 - PublicVars.health;
        }
    }
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("FoxChild")) {
            setFood();
        }
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > interval && foxAlive) {
            PublicVars.health -= deplete * 5;
            timer = 0f;
        }
        if(PublicVars.health <=0){
            foxAlive = false;
            // restartButton.SetActive(true);
            lostUI.enabled = true;
            Cursor.lockState = CursorLockMode.None;
            this.gameObject.GetComponent<PlayerController>().activeMovement = false;
            this.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        }

        if(PublicVars.dayCount > 5) {
            // restartButton.SetActive(true);
            wonUI.enabled = true;
            foxAlive = false;
            Cursor.lockState = CursorLockMode.Confined;
            this.gameObject.GetComponent<PlayerController>().activeMovement = false;
            this.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        }

        mushroomUI.text = "Mushroom Count: " + PublicVars.mushroomCount;
        berryUI.text = "Berry Count: " + PublicVars.berryCount;
        appleUI.text = "Apple Count: " + PublicVars.appleCount;


        healthUI.text = "Child Health: " + PublicVars.health;
        dayUI.text = "Day: " + PublicVars.dayCount;
    }
}