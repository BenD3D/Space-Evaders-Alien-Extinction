using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;



public class PlayerController : MonoBehaviour
{

    public float speed = 30f;
    public float turnSpeed = 25f;
    public float horizontalInput;
    public float forwardInput;

    public GameObject Spaceship;

    public GameObject ProjectilePrefab;
    public float shootCooldown = 0.2f;
    private float shootTimer = 0f;
    public float destroyDelay = 2f;
   
    public List<GameObject> LiveProjectiles;
    
    private GameManagerScript gameManager;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");

        transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput);
        transform.Rotate(Vector3.up, Time.deltaTime * turnSpeed * horizontalInput);

        shootTimer -= Time.deltaTime;


        if (Input.GetKeyDown(KeyCode.Space) && shootTimer <= 0f && gameManager.isGameActive)
        {
            GameObject Projectile = Instantiate(ProjectilePrefab, transform.position, Quaternion.Euler(0, 90, 0) * Spaceship.transform.rotation);
            shootTimer = shootCooldown;
            LiveProjectiles.Add(Projectile);

        }
    }
        
        
        
    
}
