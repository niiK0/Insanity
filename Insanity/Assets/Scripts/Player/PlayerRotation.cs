using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    //visual transforms for character and weapon rotation properly
    [SerializeField] private Transform cameraHolderTransform;
    [SerializeField] public Transform weaponTransform;

    public float mouseSensitivity = 35f;
    float xRotation = 0f;
    private Vector2 mouseDelta;

    //get the gameinput script for input stuff
    [SerializeField] private GameInput gameInput;

    void LateUpdate()
    {
        if (Time.timeScale == 0)
        {
            return;
        }

        mouseDelta = gameInput.GetMouseVector() * Time.deltaTime * mouseSensitivity;

        xRotation -= mouseDelta.y;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        cameraHolderTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseDelta.x);
    }

    //private void FixedUpdate()
    //{
    //    cameraHolderTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    //    transform.Rotate(Vector3.up * mouseDelta.x);
    //}
}
