using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeetleManager : MonoBehaviour
{
    PlayerMovement player;
    public MenuManager _menuManager;

    public int maxHealth;
    public int currentHealth;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    public int beetleGauge;

    public List<GameObject> totalCollectibles = new List<GameObject>();
    List<GameObject> collectibles = new List<GameObject>();
    public Text collectibleCounter;

    public Text goalDistanceText;
    public GameObject goal;

    public GameObject flightImage;
    private float maxFlightDuration;
    private bool drain = false;

    public Text totalCollectiblesText;
    private int counter = 0;

    public void MeasureDistance()
    {
        float distance = Vector3.Distance(goal.transform.position, transform.position);
        goalDistanceText.text = (int)distance + " m";
    }

    public void ToggleFlightImage()
    {
        flightImage.SetActive(true);
        drain = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            currentHealth -= 1;
        }

        if (other.gameObject.tag == "DeathZone")
        {
            currentHealth = 0;
        }

        if (other.gameObject.tag == "Checkpoint")
        {
            player.SetChecpoint(other.transform.position);
        }

        if (other.gameObject.tag == "Stuck")
        {
            player.ReturnToPosition();
        }

        if (other.gameObject.tag == "Collectible")
        {
            collectibles.Add(other.gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Stuck")
        {
            player.ReturnToPosition();
        }
    }

    private void UpdateHealth()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < currentHealth)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
        }
    }

    public void Start()
    {
        _menuManager = _menuManager.GetComponent<MenuManager>();

        player = GetComponent<PlayerMovement>();
        maxFlightDuration = player.maxFlightDuration;

        //sets health at maximum
        currentHealth = maxHealth;

    }

    public void Update()
    {
        if (_menuManager.isPaused == false)
        {
            //updates distance between player and end goal
            MeasureDistance();
            //updates health if current health is lowered
            UpdateHealth();
            //updates flight icon on ui whenever player is/isn't flying
            UpdateFlightIcon();
            //updates number of collectibles collected during run
            UpdateCollectibleCounter();
        }
    }

    //when player is flying, flight image is enabled and 'drains' according to flight duration
    private void UpdateFlightIcon()
    {
        if (flightImage.activeSelf)
        {
            Image flightIcon = flightImage.GetComponent<Image>();
            if (flightIcon.fillAmount > 0 && drain)
            {
                flightIcon.fillAmount -= 1 / maxFlightDuration * Time.deltaTime;
            }
            
            if (flightIcon.fillAmount <= 0)
            {
                drain = false;
                flightImage.SetActive(false);
            }
        }
        else
        {
            flightImage.GetComponent<Image>().fillAmount = 1;
        }
    }

    //whenever the player collects a collectible, updates the counter based on how many have been collected
    private void UpdateCollectibleCounter()
    {
        counter = collectibles.Count;
        collectibleCounter.text = $"{counter:00}";
    }

    public void FinalStats()
    {
        totalCollectiblesText.text = $"You collected {counter} / {totalCollectibles.Count} Collectibles";
    }
}
