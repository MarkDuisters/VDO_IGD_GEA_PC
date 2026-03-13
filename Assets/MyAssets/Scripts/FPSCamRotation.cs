using UnityEngine;
using UnityEngine.InputSystem;

public class FPSCamRotation : MonoBehaviour
{
    [SerializeField] Transform cameraRef;

    Vector2 mouseDelta;

    [SerializeField] float mouseSensitivity = 10f;
    [SerializeField] Vector2 lookLimits = new Vector2(-90f, 90f);

    float tiltRotation = 0f;
    float yAxisRotation = 0f;

    void Start()
    {
        tiltRotation = cameraRef.rotation.eulerAngles.x;
        yAxisRotation = cameraRef.rotation.eulerAngles.y;
    }

    void Update()
    {
        Vector3 currentRotation = cameraRef.rotation.eulerAngles;
        float x, y;
        x = mouseDelta.x * mouseSensitivity * Time.deltaTime;
        y = mouseDelta.y * mouseSensitivity * Time.deltaTime;
        //Vertical rotation
        tiltRotation -= y;
        tiltRotation = Mathf.Clamp(tiltRotation, lookLimits.x, lookLimits.y);
        currentRotation.x = tiltRotation;
        //Horizontal rotatoin
        yAxisRotation += x;
        currentRotation.y = yAxisRotation;

        cameraRef.rotation = Quaternion.Euler(currentRotation);
    }

    void OnLook(InputValue context)
    {
        mouseDelta = context.Get<Vector2>();
    }
}
