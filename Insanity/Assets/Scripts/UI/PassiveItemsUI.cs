using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PassiveItemsUI : MonoBehaviour
{
    [SerializeField] private Transform itemContainer;
    [SerializeField] private Transform itemTemplate;

    [SerializeField] private TextMeshProUGUI foodUI;

    [SerializeField] private TextMeshProUGUI sanityPillUI;
    [SerializeField] private TextMeshProUGUI insanityPillUI;

    public static PassiveItemsUI instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        itemTemplate.gameObject.SetActive(false);
    }

    public void UpdateItemUI(Item item)
    {
        Transform selectedUIItem = null;

        for (int i = 0; i < itemContainer.childCount; i++)
        {
            Debug.Log("Checking if hovertip item: " + itemContainer.GetChild(i).GetComponent<HoverTip>().item.name + " | item: " + item.name);
            if (itemContainer.GetChild(i).GetComponent<HoverTip>().item.name == item.name)
            {
                selectedUIItem = itemContainer.GetChild(i).transform;
                break;
            }
        }

        if(selectedUIItem != null)
        {
            Debug.Log("Increasing quantity instead on UI");
            selectedUIItem.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = item.quantity.ToString();
        }
        else
        {
            Debug.Log("Not found, adding new item to UI instead");
            AddItemToUI(item);
        }
    }

    public void AddItemToUI(Item item)
    {
        Transform itemTransform = Instantiate(itemTemplate, itemContainer);
        itemTransform.gameObject.SetActive(true);
        itemTransform.GetChild(0).GetComponent<Image>().sprite = item.icon;
        itemTransform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = item.quantity.ToString();
        HoverTip itemHover = itemTransform.gameObject.GetComponent<HoverTip>();
        itemHover.item = item;
    }

    public void UpdateAllUsableUI(int foodQuantity, int sanityQuantity, int insanityQuantity)
    {
        UpdateFoodItemUI(foodQuantity);
        UpdateSanityPillUI(sanityQuantity);
        UpdateInsanityPillUI(insanityQuantity);
    }

    public void UpdateFoodItemUI(int quantity)
    {
        foodUI.text = quantity.ToString();
    }

    public void UpdateSanityPillUI(int quantity)
    {
        sanityPillUI.text = quantity.ToString();
    }

    public void UpdateInsanityPillUI(int quantity)
    {
        insanityPillUI.text = quantity.ToString();
    }
}