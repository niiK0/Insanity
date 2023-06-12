using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    private int RandItem = 0;
    public GameObject[] Items;

    public bool RoomCleared = false;
    private bool doneObjective = false;

    //Spawner variables
    public float radius;

    // Update is called once per frame
    void Update()
    {
        if (RoomCleared && !doneObjective)
        {
            doneObjective = true;
            RandItem = Random.Range(0, Items.Length);
            Instantiate(Items[RandItem], transform.position, Quaternion.identity);
            RoomCleared=false;
        }
    }
}
