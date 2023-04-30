using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToBoss : MonoBehaviour
{
    private bool AlreadyChecked = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !AlreadyChecked)
        {
            //Go to boss scene
            SceneManager.LoadScene(2);

            //Already checked stops mobs from spawning again
            AlreadyChecked = true;
        }
    }
}
