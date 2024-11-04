using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovementBehaviour : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 100f;
    private Rigidbody rb;
    public Transform cam;
    public Vector2 sensitivity;

    // Variables de "shake" 
    public float shakeIntensity = 0.05f; 
    public float shakeSpeed = 3f; 
    private Vector3 originalCameraPosition;

    
    private bool isMoving = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //Cursor.lockState = CursorLockMode.Locked;

        // Guardar la posicion original de la camara para restaurarla después del "shake"
        originalCameraPosition = cam.localPosition;
    }

    void Update()
    {
        // Movimiento 
        float moveForward = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        float moveLateral = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;

        
        transform.Translate(moveLateral, 0, moveForward);

        // Detener el movimiento si no hay input
        if (moveForward == 0 && moveLateral == 0)
        {
            rb.linearVelocity = Vector3.zero;
            isMoving = false; 
        }
        else
        {
            isMoving = true; 
        }

        UpdateMouseLook();

        // Aplicar el shake si el personaje se mueve
        if (isMoving)
        {
            ApplyCameraShake();
        }
        else
        {
            // Si el personaje esta parado restaura camara
            cam.localPosition = originalCameraPosition;
        }
    }

    private void UpdateMouseLook()
    {
        float hor = Input.GetAxis("Mouse X");
        float ver = Input.GetAxis("Mouse Y");

        // Rotacion horizontal del personaje
        if (hor != 0)
        {
            transform.Rotate(0, hor * sensitivity.x, 0);
        }

        // Rotacion vertical de la camara
        if (ver != 0)
        {
            Vector3 rotation = cam.localEulerAngles;
            rotation.x = (rotation.x - ver * sensitivity.y + 360) % 360;

            // Limitar angulo de rotacion
            if (rotation.x > 80 && rotation.x < 180) { rotation.x = 80; }
            else if (rotation.x < 280 && rotation.x > 180) { rotation.x = 280; }

            cam.localEulerAngles = rotation;
        }
    }

    private void ApplyCameraShake()
    {
        // Usar PerlinNoise para movimiento suave de "shake"
        float shakeOffsetX = (Mathf.PerlinNoise(Time.time * shakeSpeed, 0) - 0.5f) * shakeIntensity;
        float shakeOffsetY = (Mathf.PerlinNoise(0, Time.time * shakeSpeed) - 0.5f) * shakeIntensity;

        // Aplicar el "shake" a la camara
        cam.localPosition = originalCameraPosition + new Vector3(shakeOffsetX, shakeOffsetY, 0f);
    }

}
