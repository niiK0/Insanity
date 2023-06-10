using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LeaveStartRoom(Item item)
    {
        GetComponent<ItemStuff>().PickupItem(item);
        //load maze scene
        SceneManager.LoadScene(2);
        PlayerSingle.instance.GetComponent<SanityStatsScale>().UpdateHealth();
        PlayerSingle.instance.GetComponent<SanityStatsScale>().UpdateText();
    }
}
