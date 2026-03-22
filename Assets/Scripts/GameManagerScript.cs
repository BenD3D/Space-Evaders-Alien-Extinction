using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using TMPro;

public class GameManagerScript : MonoBehaviour
{
    public List<GameObject> planets;
    public float spawnRate = 2.5f;
    public float planetlimit = 10f;
    public float planetCount = 0f;
    public bool isGameActive = true;

    public TextMeshProUGUI ScoreText;
    public int score;

    public int loopcount;
    public List<GameObject> spawnedPlanets;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        score = 0;
        UpdateScore(0);
        

        while (loopcount < 20)
        {
            Debug.Log("Spawning initial Planets...");
            int index = Random.Range(0, planets.Count);
            int spawnX = Random.Range(-550, 550);
            int spawnZ = Random.Range(-550, 550);
            GameObject planet = planets[index];
            planet.transform.position = new Vector3(spawnX, 0, spawnZ);
            Debug.Log(planet.name + " Has been Spawned");
            spawnedPlanets.Add(planet);
            planetCount = planetCount + 1f;
            loopcount = loopcount + 1;
            //if (planet.transform.position !=  new Vector3(spawnX, 0, 0))
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        ScoreText.text = "Score: " + score;
    }

   
}
