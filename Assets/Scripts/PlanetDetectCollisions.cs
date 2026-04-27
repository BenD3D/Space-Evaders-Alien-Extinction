using System.Collections.Generic;
using UnityEngine;

public class PlanetDetectCollisions : MonoBehaviour
{
    public ParticleSystem explosionParticle;
    private GameManagerScript gameManager;
    public List<GameObject> planetslist;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if(!gameManager.AliensList.Contains(other.gameObject))
        {
            Destroy(other.gameObject);
            return;
        }

        gameManager.DestroyPlanet(gameObject);
        //Destroy(gameObject);
        //gameManager.TotalPlanets(1);

        
    }
}
