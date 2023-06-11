using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeTrack : MonoBehaviour
{
    public Transform playerTransform; // Reference to the player's transform

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(playerTransform);
    }
}
