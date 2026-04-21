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
    public int alienmovementSpeed = 50;

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

    void Start()
    {
        isGameActive = false;

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
        
        while (alien != null && availableplanets.Count > 0 && isGameActive)
        {

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

            if (planetCount <= 2 || availableplanets == null)
            {
                GameOver();
                Debug.Log("Game Over from Alien Spawner");
            }

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

            planetCount--;
            PlanetCountText.text = "Planets Left: " + planetCount;
            UpdateScore(100); 
        }
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        ScoreText.text = "Aliens Destroyed: " + score;
    }

    public void TotalPlanets(int count)
    {
        planetCount -= count;
        PlanetCountText.text = "Planets Left: " + planetCount;

        if (planetCount <= 2 ||  availableplanets == null)
        {
            GameOver();
            Debug.Log("Game Over from total planets");
        }
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


    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame()
    {
        isGameActive = true;
        score = 0;
        UpdateScore(0);

        GameName.gameObject.SetActive(false);
        EasyButton.gameObject.SetActive(false);
        MediumButton.gameObject.SetActive(false);
        HardButton.gameObject.SetActive(false);

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
}