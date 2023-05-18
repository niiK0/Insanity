using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PassiveItemsUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemDesc;
    [SerializeField] private Transform itemContainer;
    [SerializeField] private Transform itemTemplate;

    private void Awake()
    {
        itemTemplate.gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
