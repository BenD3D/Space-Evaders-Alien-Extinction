using UnityEngine;

public class RocketMove : MonoBehaviour
{
    public GameObject Spaceship;
    public float speed = 20f;

    public Vector3 NorthBound = new Vector3(0, 0, 1000);
    public Vector3 EastBound = new Vector3(1000, 0, 0);
    public Vector3 SouthBound = new Vector3(0, 0, -1000);
    public Vector3 WestBound = new Vector3(-1000, 0, 0);

    public Vector3 direction = Vector3.forward;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * Time.deltaTime * speed);

        if (transform.position.z > NorthBound.z || transform.position.z < SouthBound.z || transform.position.x > EastBound.x || transform.position.x < WestBound.x)
        {
            Destroy(gameObject);
        }
    }
}
