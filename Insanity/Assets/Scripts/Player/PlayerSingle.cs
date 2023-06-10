using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSingle : MonoBehaviour
{
    public static PlayerSingle instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
