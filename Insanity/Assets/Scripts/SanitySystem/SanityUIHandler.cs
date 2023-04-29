using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using SanitySystem;

public class SanityUIHandler : MonoBehaviour
{
    public TMP_Text sanityText;
    public SanityStatsScale sanityScale;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Change later depending on enemy die and so on, remove from update so it only updates on change
        //sanityText.text = sanity.sanity.ToString() + "%";
    }
}
