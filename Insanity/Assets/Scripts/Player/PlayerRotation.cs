using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    //visual transforms for character and weapon rotation properly
    [SerializeField] private Transform cameraHolderTransform;
    [SerializeField] public Transform weaponTransform;

    public float mouseSensitivity = 100f;
    float xRotation = 0f;

    //get the gameinput script for input stuff
    [SerializeField] private GameInput gameInput;

    void Update()
    {
        if (Time.timeScale == 0)
        {
            return;
        }

        Vector2 mouseDelta = gameInput.GetMouseVector() * Time.deltaTime * mouseSensitivity;

        xRotation -= mouseDelta.y;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        cameraHolderTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseDelta.x);
        //weaponTransform.localRotation = Quaternion.Euler(new Vector3(0, Camera.main.transform.rotation.eulerAngles.y, 0));
    }
}
