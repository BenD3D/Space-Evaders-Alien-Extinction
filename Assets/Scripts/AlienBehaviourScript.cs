using UnityEngine;
using System.Collections.Generic;

public class AlienBehaviourScript : MonoBehaviour
{
    public List<GameObject> AliensList;
    public List<GameObject> AssignedAliensList;
    public GameManagerScript gameManager;
    
    void Start()
    {
        
        List<GameObject> availablePlanets = new List<GameObject>(gameManager.spawnedPlanets);

        int aliensToAssign = 10;


        while (aliensToAssign > 0 && AliensList.Count > 0 && availablePlanets.Count > 0)
        {
            
            int alienIndex = Random.Range(0, AliensList.Count);
            int planetIndex = Random.Range(0, availablePlanets.Count);

            GameObject currentAlien = AliensList[alienIndex];
            GameObject targetPlanet = availablePlanets[planetIndex];

            currentAlien.transform.position = targetPlanet.transform.position;
            Debug.Log("Moving " + currentAlien.name + " to " + targetPlanet.name);
            AssignedAliensList.Add(currentAlien);
            AliensList.RemoveAt(alienIndex);

            availablePlanets.RemoveAt(planetIndex);

            aliensToAssign--;
        }
    }
}

// make it so that planets are already created and in a list in gamemanager, then randomised their positons,
// then assign aliens to them, and then create a moveto fucntion that moves aliens to the planets, then when it reaches the planet, it destroys it
