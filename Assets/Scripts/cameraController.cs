using UnityEngine;

public class cameraController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float mouseSensitivity = 90;

    private float xRotation;

    private void Start()
    {
        xRotation = 0;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void Update()
    {
        //get mouse imput
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;//Time.deltaTime: time between each frame
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;//-:invert
        xRotation = Mathf.Clamp(xRotation, -90, 90);

        //xRotation shold not ever be bigger than 90, or less than -90

        //rotate player around y axis, camera around x axis
        transform.parent.Rotate(Vector3.up * mouseX);
        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
    }

}