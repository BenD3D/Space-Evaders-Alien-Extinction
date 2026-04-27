using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Unity.VisualScripting;

public class GameManagerScript : MonoBehaviour
{
    public List<GameObject> availableplanets;
    public List<GameObject> AliensList;
    public GameObject sun;
    public ParticleSystem explosionParticle;

    public float planetCount;
    public int alienmovementSpeed = 40;

    public int planetsleft;

    public TextMeshProUGUI ScoreText;
    public int score;
    public TextMeshProUGUI PlanetCountText;
    public TextMeshProUGUI GameOverText;
    public bool isGameActive;

    public GameObject player;
    public Button restartbutton;

    // Title Screen
    public TextMeshProUGUI GameName;
    public Button EasyButton;
    public Button MediumButton;
    public Button HardButton;

    public TextMeshProUGUI Wintext;

    // Controls menu
    public TextMeshProUGUI ControlsTitle;
    public TextMeshProUGUI BasicMovement;
    public TextMeshProUGUI FireRocket;
    public TextMeshProUGUI Boost;
    public Button ControlsButton;
    public TextMeshProUGUI Description;
    public Button BackButton;
    

    void Start()
    {
        isGameActive = false;
        planetsleft = 20;

        if (isGameActive == false)
        {
            sun.SetActive(false);
            player.SetActive(false);
            foreach (GameObject planet in availableplanets)
            {
                planet.SetActive(false);
            }
            foreach (GameObject alien in AliensList)
            {
                alien.SetActive(false);
            }
        }

    }

    

    IEnumerator AlienBehavior(GameObject alien)
    {
        

        while (alien != null && isGameActive)
        {
            availableplanets.RemoveAll(planet => planet == null);

            if (availableplanets.Count <= 0)
            {
                GameOver();
                yield break;
            }

            GameObject targetPlanet = availableplanets[Random.Range(0, availableplanets.Count)];


            while (alien != null && targetPlanet != null && Vector3.Distance(alien.transform.position, targetPlanet.transform.position) > 1f)
            {
                alien.transform.position = Vector3.MoveTowards(
                    alien.transform.position,
                    targetPlanet.transform.position,
                    alienmovementSpeed * Time.deltaTime
                );
                yield return null;
            }

            //if (planetCount <= 2 || availableplanets == null || availableplanets.Count == 0)
            //{
            //    GameOver();
            //    Debug.Log("Game Over from Alien Spawner");
            //}

            if (alien != null && targetPlanet != null)
            {
                Debug.Log(alien.name + " destroyed " + targetPlanet.name);
                DestroyPlanet(targetPlanet);
            }


            yield return new WaitForSeconds(1f);
        }
    }

    public void DestroyPlanet(GameObject planet)
    {
        if (availableplanets.Contains(planet))
        {
            availableplanets.Remove(planet);

            Destroy(planet);

            PlanetCountText.text = "Planets Left: " + availableplanets.Count;
            
            if (availableplanets.Count <= 0)
            {
                GameOver();
            }

            
        }
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        ScoreText.text = "Aliens Destroyed: " + score;

        if (score == 10)
        {
            Victory();
            Debug.Log("You Win!");
        }
    }

    public void TotalPlanets(int count)
    {
        planetCount -= count;
        PlanetCountText.text = "Planets Left: " + planetCount;

    }


    public void GameOver()
    {
        isGameActive = false;
        GameOverText.gameObject.SetActive(true);
        restartbutton.gameObject.SetActive(true);
        ScoreText.gameObject.SetActive(false);
        PlanetCountText.gameObject.SetActive(false);
        player.SetActive(false);

        foreach (GameObject alien in AliensList)
        {
            Destroy(alien);
        }

        ParticleSystem explosion = Instantiate(
        explosionParticle,
        sun.transform.position,
        sun.transform.rotation);

        Destroy(sun);

        explosion.Play();

    }

    public void Victory()
    {
        isGameActive = false;
        Wintext.gameObject.SetActive(true);
        restartbutton.gameObject.SetActive(true);
        ScoreText.gameObject.SetActive(false);
        PlanetCountText.gameObject.SetActive(false);
        player.SetActive(false);
        Debug.Log("Victory Function Called");

    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ControlsMenu()
    {
        GameName.gameObject.SetActive(false);
        EasyButton.gameObject.SetActive(false);
        MediumButton.gameObject.SetActive(false);
        HardButton.gameObject.SetActive(false);
        ControlsButton.gameObject.SetActive(false);

        ControlsTitle.gameObject.SetActive(true);
        BasicMovement.gameObject.SetActive(true);
        FireRocket.gameObject.SetActive(true);
        Boost.gameObject.SetActive(true);
        Description.gameObject.SetActive(true);
        BackButton.gameObject.SetActive(true);
    }

    public void StartGame(int difficulty)
    {
        isGameActive = true;
        score = 0;
        UpdateScore(0);

        if (difficulty == 1)
        {
            alienmovementSpeed = 40;
        }
        else if (difficulty == 2)
        {
            alienmovementSpeed = 75;
        }
        else if (difficulty == 3)
        {
            alienmovementSpeed = 100;
        }

        GameName.gameObject.SetActive(false);
        EasyButton.gameObject.SetActive(false);
        MediumButton.gameObject.SetActive(false);
        HardButton.gameObject.SetActive(false);
        ControlsButton.gameObject.SetActive(false);

        ScoreText.gameObject.SetActive(true);
        PlanetCountText.gameObject.SetActive(true);

        sun.SetActive(true);
        player.SetActive(true);
        foreach (GameObject planet in availableplanets)
        {
            planet.SetActive(true);
        }
        foreach (GameObject alien in AliensList)
        {
            alien.SetActive(true);
        }




        for (int i = 0; i < availableplanets.Count; i++)
        {
            float spawnX = Random.Range(-600f, 600f);
            float spawnZ = Random.Range(-600f, 600f);
            availableplanets[i].transform.position = new Vector3(spawnX, 0, spawnZ);
        }



        planetCount = availableplanets.Count;
        PlanetCountText.text = "Planets Left: 20";


        foreach (GameObject alien in AliensList)
        {
            StartCoroutine(AlienBehavior(alien));
        }

    }

    void Update()
    {
        
    }

}