using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public int deplete = 1;
    bool foxAlive = true;

    private float timer = 0f;
    private float interval = 10f;

    public Image reticle;
    public TextMeshProUGUI healthUI;
    public TextMeshProUGUI berryUI;
    public TextMeshProUGUI appleUI;
    public TextMeshProUGUI mushroomUI;
    public TextMeshProUGUI dayUI;
    public TextMeshProUGUI wonUI;
    public TextMeshProUGUI lostUI;
    public GameObject restartButton;

    public AudioClip eating;
    public AudioClip win;
    public AudioClip lose;
    AudioSource _audiosource;

    bool over = false;

    private void Start()
    {
        foxAlive = true;
        restartButton.SetActive(false);
        wonUI.enabled = false;
        lostUI.enabled = false;
        reticle.enabled = true;
        over = false;

        PublicVars.health = 100;
        PublicVars.dayCount = 1;
        PublicVars.berryCount = 0;
        PublicVars.mushroomCount = 0;
        PublicVars.appleCount = 0;

        Cursor.lockState = CursorLockMode.Locked;
        this.gameObject.GetComponent<PlayerController>().activeMovement = true;
        this.gameObject.GetComponent<Rigidbody>().isKinematic = false;

        healthUI.text = "Children Health: " + PublicVars.health;
        berryUI.text = "Berry Count: " + PublicVars.berryCount;
        appleUI.text = "Apple Count: " + PublicVars.appleCount;
        mushroomUI.text = "Mushroom Count: " + PublicVars.mushroomCount;
        dayUI.text = "Day: " + PublicVars.dayCount;
        restartButton.SetActive(false);
        wonUI.enabled = false;
        lostUI.enabled = false;
        _audiosource = GetComponent<AudioSource>();
    }

    private void setFood() {
        var remainingHealth = 100 - PublicVars.health;
        bool ate = false;

        var mushroomsNeeded = (int) remainingHealth / PublicVars.mushroomHealth;
        if (mushroomsNeeded <= PublicVars.mushroomCount) {
            PublicVars.mushroomCount -= mushroomsNeeded;
            PublicVars.health += mushroomsNeeded * PublicVars.mushroomHealth;
            remainingHealth = 100 - PublicVars.health;
            if (mushroomsNeeded != 0) {
                ate = true;
            }
        } else {
            if (PublicVars.mushroomCount > 0) {
                ate = true;
            }
            PublicVars.health += PublicVars.mushroomCount * PublicVars.mushroomHealth;
            PublicVars.mushroomCount = 0;
            remainingHealth = 100 - PublicVars.health;
        }

        var applesNeeded = (int) remainingHealth / PublicVars.appleHealth;
        if (applesNeeded <= PublicVars.appleCount) {
            PublicVars.appleCount -= applesNeeded;
            PublicVars.health += applesNeeded * PublicVars.appleHealth;
            remainingHealth = 100 - PublicVars.health;
            if (applesNeeded != 0) {
                ate = true;
            }
        } else {
            if (PublicVars.appleCount > 0) {
                ate = true;
            }
            PublicVars.health += PublicVars.appleCount * PublicVars.appleHealth;
            PublicVars.appleCount = 0;
            remainingHealth = 100 - PublicVars.health;
        }

        var berriesNeeded = (int) remainingHealth / PublicVars.berryHealth;
        if (berriesNeeded <= PublicVars.berryCount) {
            PublicVars.berryCount -= berriesNeeded;
            PublicVars.health += berriesNeeded * PublicVars.berryHealth;
            remainingHealth = 100 - PublicVars.health;
            if (berriesNeeded != 0) {
                ate = true;
            }
        } else {
            if (PublicVars.berryCount > 0) {
                ate = true;
            }
            PublicVars.health += PublicVars.berryCount * PublicVars.berryHealth;
            PublicVars.berryCount = 0;
            remainingHealth = 100 - PublicVars.health;
        }
        if (ate) {
            _audiosource.PlayOneShot(eating);
        }
    }
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("FoxChild")) {
            setFood();
        }
    }

    void Update()
    {
        if (!over) {
            timer += Time.deltaTime;
            if (timer > interval && foxAlive) {
                var newHealth = PublicVars.health - deplete * 10;
                if (newHealth < 0) {
                    newHealth = 0;
                }
                PublicVars.health = newHealth;
                timer = 0f;
            }

            if(PublicVars.health <= 0){
                _audiosource.PlayOneShot(lose);
                foxAlive = false;
                restartButton.SetActive(true);
                reticle.enabled = false;
                lostUI.enabled = true;
                over = true;
                
                Cursor.lockState = CursorLockMode.None;
                this.gameObject.GetComponent<PlayerController>().activeMovement = false;
                this.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            }

            if(PublicVars.dayCount > 5) {
                _audiosource.PlayOneShot(win);
                restartButton.SetActive(true);
                wonUI.enabled = true;
                reticle.enabled = false;
                foxAlive = false;
                over = true;

                Cursor.lockState = CursorLockMode.None;
                this.gameObject.GetComponent<PlayerController>().activeMovement = false;
                this.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            }

            mushroomUI.text = "Mushroom Count: " + PublicVars.mushroomCount;
            berryUI.text = "Berry Count: " + PublicVars.berryCount;
            appleUI.text = "Apple Count: " + PublicVars.appleCount;


            healthUI.text = "Children Health: " + PublicVars.health;
            dayUI.text = "Day: " + PublicVars.dayCount;
        }
    }
}