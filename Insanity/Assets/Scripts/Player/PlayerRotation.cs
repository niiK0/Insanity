using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    //visual transforms for character and weapon rotation properly
    [SerializeField] private Transform visualTransform;
    [SerializeField] public Transform weaponTransform;

    //get the gameinput script for input stuff
    [SerializeField] private GameInput gameInput;

    public float sensitivity = 100.0f;
    public float smoothing = 5.0f;

    Vector2 mouseLook;
    Vector2 smoothV;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (Time.timeScale == 0)
        {
            return;
        }
        //Vector2 mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        //mouseDelta = Vector2.Scale(mouseDelta, new Vector2(sensitivity * smoothing, sensitivity * smoothing));
        //smoothV.x = Mathf.Lerp(smoothV.x, mouseDelta.x, 1f / smoothing);
        //smoothV.y = Mathf.Lerp(smoothV.y, mouseDelta.y, 1f / smoothing);
        //mouseLook += smoothV;

        //mouseLook.y = Mathf.Clamp(mouseLook.y, -90, 90);

        //Camera.main.transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
        visualTransform.rotation = Quaternion.Euler(new Vector3(0, Camera.main.transform.rotation.eulerAngles.y, 0));
        weaponTransform.rotation = Quaternion.Euler(new Vector3(0, Camera.main.transform.rotation.eulerAngles.y, 0));
    }
}
