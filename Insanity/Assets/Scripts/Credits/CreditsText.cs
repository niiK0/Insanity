using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsText : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float duration = 30f;

    private float elapsedTime = 0f;
    private RectTransform textTransform;

    void Start()
    {
        textTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime < duration)
        {
            float distance = moveSpeed * Time.deltaTime;
            textTransform.position += new Vector3(0f, distance, 0f);
        }
        else if(elapsedTime >= duration) 
        {
            //Go to boss scene
            SceneManager.LoadScene(0);
        }
    }
}
