using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player; 
    public Vector3 offset = new Vector3(-3.84f, 4.16f,-19.36146f);

    public float rotationSpeed = 100f; 

    // private float HorizontalInput;
    // private float ForwardInput;

    private float MouseX;
    private float MouseY;
    void Start()
    {
        Cursor.visible = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Locked;
            new WaitForSeconds(5f);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    void LateUpdate()
    {
        // HorizontalInput = Input.GetAxis("Horizontal");
        // ForwardInput = Input.GetAxis("Vertical");
        MouseX = Input.GetAxis("Mouse X");
        MouseY = Input.GetAxis("Mouse Y");

        transform.position = player.transform.position + offset; 
        HandleCamera();
        transform.LookAt(player.transform); 
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
