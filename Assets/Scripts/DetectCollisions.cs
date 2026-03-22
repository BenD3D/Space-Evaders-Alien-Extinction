using System;
using UnityEngine;

public class DetectCollisions : MonoBehaviour
{
    public ParticleSystem explosionParticle;
    private GameManagerScript gameManager;
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
        ParticleSystem explosion = Instantiate(
        explosionParticle, 
        transform.position, 
        transform.rotation);

        explosion.Play();

        Destroy(gameObject);
        Destroy(other.gameObject);
        gameManager.UpdateScore(1);
        gameManager.planetCount = gameManager.planetCount - 1f;
    }
}
