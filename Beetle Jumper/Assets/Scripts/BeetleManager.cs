using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeetleManager : MonoBehaviour
{
    #region References
    [Header("References")]
    public MenuManager _menuManager;
    PlayerMovement player;

    [Header("Player Health")]
    public int maxHealth;
    public int currentHealth;

    public GameObject heartsContainerUI;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    [Header("Collectibles")]
    List<GameObject> collectibles = new List<GameObject>();
    public Text collectibleCounter;

    public GameObject collectUI;
    public Text totalCollectiblesText;  
    public List<GameObject> totalCollectibles = new List<GameObject>();
    private int counter = 0;

    [Header("Distance To Goal")]
    public Text goalDistanceText;
    public GameObject goal;

    [Header("Flight")]
    public GameObject flightImage;
    private float maxFlightDuration;
    private bool drain = false;

    [Header("Player Status")]
    public bool inWater;
    #endregion

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

    #region OnTrigger
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            currentHealth -= 1;
            StartCoroutine(nameof(ShowHideHealth));
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
            StartCoroutine(nameof(ShowHideCollect));
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Stuck")
        {
            player.ReturnToPosition();
        }

        if (other.gameObject.tag == "Water")
        {
            inWater = true;
        }
    }
    #endregion

    #region Health System
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

    IEnumerator ShowHideHealth()
    {
        heartsContainerUI.SetActive(true);

        yield return new WaitForSeconds(3f);

        heartsContainerUI.SetActive(false);
    }
    #endregion

    #region Flight System
    public void ToggleFlightImage()
    {
        if (flightImage.GetComponent<Image>().fillAmount == 1)
        {
            drain = true;
        }
    }

    //when player is flying, flight image is enabled and 'drains' according to flight duration
    private void UpdateFlightIcon()
    {
        if (drain)
        {
            flightImage.SetActive(true);
            Image flightIcon = flightImage.GetComponent<Image>();
            if (flightIcon.fillAmount > 0)
            {
                flightIcon.fillAmount -= 1 / maxFlightDuration * Time.deltaTime;
            }

            if (flightIcon.fillAmount <= 0)
            {
                drain = false;
            }
        }

        if (!drain)
        {
            float IconFill = flightImage.GetComponent<Image>().fillAmount;
            flightImage.GetComponent<Image>().fillAmount += Time.deltaTime;
            if (IconFill == 1)
            {
                flightImage.SetActive(false);
            }
        }
    }
    #endregion

    #region Collectibles
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
    IEnumerator ShowHideCollect()
    {
        collectUI.SetActive(true);

        yield return new WaitForSeconds(3f);

        collectUI.SetActive(false);
    }
    #endregion

    #region Distance
    public void MeasureDistance()
    {
        float distance = Vector3.Distance(goal.transform.position, transform.position);
        goalDistanceText.text = $"Distance to Goal\n{(int)distance}cm";
    }
    #endregion

    #region Player Status
    private void UpdatePlayerStatus()
    {
        if (inWater)
        {
            player.canJump = false;
        }
        else
        {
            player.canJump = true;
        }
    }

    public void OutOfWater()
    {
        inWater = false;
    }
    #endregion
}
