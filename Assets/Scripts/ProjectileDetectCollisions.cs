using System.Collections.Generic;
using UnityEngine;

public class ProjectileDetectCollisions : MonoBehaviour
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

        //if (!planetslist.Contains(other.gameObject))
        //{
        //    return;
        //}

        ParticleSystem explosion = Instantiate(
        explosionParticle,
        transform.position,
        transform.rotation);

        explosion.Play();

        Destroy(gameObject);
        Destroy(other.gameObject);
        
        
    }
}
