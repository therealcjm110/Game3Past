using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float sensX;
    public float sensY;

    public Transform orientation;

    float xRotation;
    float yRotation;

    private void Start()
    {
        if (Time.timeScale > 0)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    private void Update()
    {

        if (Time.timeScale == 0 || Cursor.lockState != CursorLockMode.Locked) return;

        float sensitivityMultiplier = PlayerPrefs.GetFloat("MouseSensitivity", 1f);

        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX * sensitivityMultiplier;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY * sensitivityMultiplier;

        yRotation += mouseX;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}