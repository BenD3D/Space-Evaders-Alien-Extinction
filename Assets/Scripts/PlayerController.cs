using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using TMPro;


public class PlayerController : MonoBehaviour
{

    public float speed = 100f;
    public float turnSpeed = 25f;
    public float horizontalInput;
    public float forwardInput;

    public float acceleration = 60f;
    public float drag = 2f;
    private float currentVelocity = 0f;

    public GameObject Spaceship;

    public GameObject ProjectilePrefab;
    public float shootCooldown = 0.2f;
    private float shootTimer = 0f;
    public float destroyDelay = 2f;
   
    public List<GameObject> LiveProjectiles;
    
    private GameManagerScript gameManager;

    public TextMeshProUGUI SpeedDisplayText;
    public TextMeshProUGUI BoostNotifier;

    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.isGameActive)
        {
            SpeedDisplayText.gameObject.SetActive(true);
        }

        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");

        float currentSpeed = speed;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed *= 1.45f; 
            BoostNotifier.gameObject.SetActive(true);
        }
        else
        {
            BoostNotifier.gameObject.SetActive(false);
        }

        if (forwardInput != 0)
        {
            currentVelocity += forwardInput * acceleration * Time.deltaTime;
            currentVelocity = Mathf.Clamp(currentVelocity, -currentSpeed, currentSpeed);

        }
        else
        {
            currentVelocity = Mathf.Lerp(currentVelocity, 0, drag * Time.deltaTime);
        }

        transform.Translate(Vector3.forward * Time.deltaTime * currentVelocity);
        transform.Rotate(Vector3.up, Time.deltaTime * turnSpeed * horizontalInput);

        shootTimer -= Time.deltaTime;

        float speedDisplayed = Mathf.Round(currentVelocity);

        if (Input.GetKeyDown(KeyCode.Space) && shootTimer <= 0f && gameManager.isGameActive)
        {
            GameObject Projectile = Instantiate(ProjectilePrefab, transform.position, Quaternion.Euler(0, 90, 0) * Spaceship.transform.rotation);
            Projectile.SetActive(true);
            shootTimer = shootCooldown;
            LiveProjectiles.Add(Projectile);

        }

        SpeedDisplayText.text = "Speed: " + speedDisplayed + " m/s";

    }
        
        
        
    
}
