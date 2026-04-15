using System.Collections.Generic;
using UnityEngine;

public class ProjectileDetectCollisions : MonoBehaviour
{

    public ParticleSystem explosionParticle;
    private GameManagerScript gameManager;
    private PlanetDetectCollisions PlanetCollisions;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
        PlanetCollisions = GetComponent<PlanetDetectCollisions>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {

        //if (PlanetCollisions.planetslist.Contains(other.gameObject) && other.gameObject.name.Contains("Projectile"))
        //{
        //    Destroy(gameObject);
        //    return;
        //}

        
        
        
        //Debug.Log("Projectile collided with: " + other.gameObject.name);

        //Destroy(gameObject);
        //Destroy(other.gameObject);
        
        
        
        
    }
}
