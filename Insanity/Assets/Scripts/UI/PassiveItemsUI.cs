using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PassiveItemsUI : MonoBehaviour
{
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

    public void AddItemToUI(Item item)
    {
        Transform itemTransform = Instantiate(itemTemplate, itemContainer);
        itemTransform.gameObject.SetActive(true);
        itemTransform.GetChild(0).GetComponent<Image>().sprite = item.icon;
        HoverTip itemHover = itemTransform.gameObject.GetComponent<HoverTip>();
        itemHover.tipTitle = item.name;
        itemHover.tipDesc = item.desc;
    }
}
