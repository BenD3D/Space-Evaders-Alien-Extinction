using System.Collections.Generic;
//using UnityEditor.EditorTools;
using UnityEngine;

public class AlienDetectCollisons : MonoBehaviour
{
    public ParticleSystem explosionParticle;
    private GameManagerScript gameManager;
    public List<GameObject> AlienslistCollisons;
    public GameObject projectile;
    private PlayerController playerController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
        playerController = GameObject.Find("SpaceshipFINAL").GetComponent<PlayerController>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {

        if (AlienslistCollisons.Contains(other.gameObject))
        {
            return;
        }

        ParticleSystem explosion = Instantiate(
        explosionParticle,
        transform.position,
        transform.rotation);

        explosion.Play();

        Destroy(other.gameObject);
        
        if (playerController.LiveProjectiles.Contains(other.gameObject))
        {
            Destroy(gameObject);
            gameManager.AliensList.Remove(gameObject);
            Debug.Log("Alien Destroyed");
            gameManager.UpdateScore(1);
        }

        
        
    }
}

