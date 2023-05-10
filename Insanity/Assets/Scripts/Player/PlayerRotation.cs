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

    // Update is called once per frame
    void Update()
    {
        //gets the mouseinput vector
        //creates a ray of type screenpointtoray, makes a plane on the player's position so there's no elevation issues for the pointing.
        //makes a raycast with the ray to the plane to check where is it aiming
        //gets the position of the hit and makes the character look at it
        //also forcing its and the weapon's rotation to the quaternion euler of the character y angle
        Vector2 mouseInput = gameInput.GetMouseVector();
        Ray mouseRay = Camera.main.ScreenPointToRay(new Vector3(mouseInput.x, mouseInput.y, 0));
        Plane p = new Plane(Vector3.up, transform.position);
        if (p.Raycast(mouseRay, out float hitDist))
        {
            Vector3 hitPoint = mouseRay.GetPoint(hitDist);
            visualTransform.LookAt(hitPoint);
            visualTransform.rotation = Quaternion.Euler(new Vector3(0, visualTransform.rotation.eulerAngles.y, 0));
            weaponTransform.rotation = Quaternion.Euler(new Vector3(0, visualTransform.rotation.eulerAngles.y, 0));
        }
    }
}
