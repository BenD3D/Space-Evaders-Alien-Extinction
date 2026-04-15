using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player; 
    public Vector3 offset = new Vector3(-3.84f, 4.16f,-19.36146f);

    public float rotationSpeed = 100f;

    private GameManagerScript gameManager;

    // private float HorizontalInput;
    // private float ForwardInput;

    private float MouseX;
    private float MouseY;
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            new WaitForSeconds(5f);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        if (gameManager.isGameActive == false)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        if (gameManager.isGameActive == true)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

    }

    void LateUpdate()
    {
        if (gameManager.isGameActive)
        {
            MouseX = Input.GetAxis("Mouse X");
            MouseY = Input.GetAxis("Mouse Y");

            transform.position = player.transform.position + offset;
            HandleCamera();
            transform.LookAt(player.transform);
        }
    }

    void HandleCamera()
    {
        float rotation = 0f;
        if (MouseX > 0 || MouseX < 0)
        {
            rotation = MouseX * rotationSpeed * Time.deltaTime;
        }
        else if (MouseY > 0 || MouseY < 0)
        {
            rotation = -MouseY * rotationSpeed * Time.deltaTime;
        }
        
        if (rotation != 0f)
        {
            offset = Quaternion.AngleAxis(rotation, Vector3.up) * offset; 
        }

        
    }
  
}
