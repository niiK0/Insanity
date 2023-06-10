using StatSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsableItems : MonoBehaviour
{
    public int sanityPillsAmount, insanityPillsAmount, foodItemAmount;

    public int sanityValue = 5;
    public int insanityValue = 5;
    public int foodValue = 20;

    //get the gameinput script for input stuff
    [SerializeField] private GameInput gameInput;

    //get the stat controller for basically everything related to stats
    private Transform player;
    protected StatController playerStatController;
    protected virtual void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerStatController = player.GetComponent<StatController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        PassiveItemsUI.instance.UpdateAllUsableUI(foodItemAmount, sanityPillsAmount, insanityPillsAmount);
        gameInput.OnTakeFood += GameInput_OnTakeFood;
        gameInput.OnTakeInsanityPill += GameInput_OnTakeInsanityPill;
        gameInput.OnTakeSanityPill += GameInput_OnTakeSanityPill;
    }

    private void GameInput_OnTakeSanityPill()
    {
        if (sanityPillsAmount > 0)
        {
            sanityPillsAmount--;
            player.GetComponent<SanityStatsScale>().TakeSanityPill(sanityValue);
            PassiveItemsUI.instance.UpdateSanityPillUI(sanityPillsAmount);
        }
    }

    private void GameInput_OnTakeInsanityPill()
    {
        if(insanityPillsAmount > 0)
        {
            insanityPillsAmount--;
            player.GetComponent<SanityStatsScale>().TakeInsanityPill(insanityValue);
            PassiveItemsUI.instance.UpdateInsanityPillUI(insanityPillsAmount);
        }
    }

    private void GameInput_OnTakeFood()
    {
        if(foodItemAmount > 0)
        {
            foodItemAmount--;
            player.GetComponent<SimpleHealth>().TakeFood(foodValue);
            PassiveItemsUI.instance.UpdateFoodItemUI(foodItemAmount);
        }
    }
}

