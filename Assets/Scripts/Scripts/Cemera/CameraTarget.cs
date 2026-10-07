using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraTarget : MonoBehaviour
{
    private GameObject Player;
    private PlayerInput playerInput;
    private InputAction cameraAction;
    float yaw;
    float pitch;
    private float rotationYawPower = 0.1f;
    private float rotationPithPower = 0.05f;
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        playerInput = GetComponent<PlayerInput>();
        cameraAction = playerInput.actions.FindAction("Camera/CameraInput");
    }

    // Update is called once per frame
    void Update()
    {
        if (Player == null)
        {
            return;
        }
        this.transform.position = Player.transform.position;
        Vector2 mouseDelta = cameraAction.ReadValue<Vector2>();
        yaw += mouseDelta.x * rotationYawPower;
        pitch += -mouseDelta.y * rotationPithPower;
        pitch = Mathf.Clamp(pitch,-70,70);
        //yaw = Mathf.Clamp(yaw, -180, 180);
        //transform.rotation *= Quaternion.AngleAxis(yaw, Vector3.up);
        //transform.rotation *= Quaternion.AngleAxis(pitch, Vector3.right);
        transform.rotation = Quaternion.Euler(pitch, yaw, 0);
    }
}
