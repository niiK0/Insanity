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
        if(SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(4))
        {
            Destroy(PlayerSingle.instance.gameObject);
            Destroy(AudioManager.instance.gameObject);
            Destroy(PassiveItemsUI.instance.gameObject);
            Destroy(gameObject);
        }
    }

    public void LeaveStartRoom(Item item)
    {
        GetComponent<ItemStuff>().PickupItem(item);
        //load maze scene
        SceneManager.LoadScene(2);
        PlayerSingle.instance.GetComponent<SanityStatsScale>().UpdateHealth();
        PlayerSingle.instance.GetComponent<SanityStatsScale>().UpdateText();
        PlayerSingle.instance.transform.position = new Vector3(0f, 2f, 0f);
    }
}
