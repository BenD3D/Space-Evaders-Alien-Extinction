using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using TMPro;

public class GameManagerScript : MonoBehaviour
{
    public List<GameObject> availableplanets;
    public List<GameObject> AliensList;
    public GameObject sun;
    public ParticleSystem explosionParticle;

    public float planetCount = 20;
    public int alienmovementSpeed = 50;

    public TextMeshProUGUI ScoreText;
    public int score;
    public TextMeshProUGUI PlanetCountText;
    public TextMeshProUGUI GameOverText;
    public bool isGameActive;

    public GameObject player;

    void Start()
    {
        isGameActive = true;
        score = 0;
        UpdateScore(0);

        

        if (isGameActive)
        {
            ScoreText.gameObject.SetActive(true);
            PlanetCountText.gameObject.SetActive(true);
            
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

        if (planetCount <= 0)
        {
            GameOver();
        }
    }


    public void GameOver()
    {
        isGameActive = false;
        GameOverText.gameObject.SetActive(true);
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
}