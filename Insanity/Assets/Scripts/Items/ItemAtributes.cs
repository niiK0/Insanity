using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAtributes : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Fixe corre o ficheiro do niko
            Destroy(gameObject);
        }
    }
}
