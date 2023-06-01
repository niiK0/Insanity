using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    private int RandItem = 0;
    public GameObject[] Items;

    public bool RoomCleared = false;

    //Spawner variables
    public float radius;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (RoomCleared)
        {
            RandItem = Random.Range(0, Items.Length);
            Instantiate(Items[RandItem], this.transform.position, Quaternion.identity);
            RoomCleared= false;
        }
    }
}
