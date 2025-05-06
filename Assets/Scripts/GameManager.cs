using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Dish currentDish;
    public List<CardBehaviour> allCards;
    private void Awake()
    {
        instance = this;
    }
    [ContextMenu("ShuffleRQ")]
    public void FirstDealCards()
    {
        allCards = ShuffleList(allCards);
        int ingredients = 0;
        int flavor = 0;
        for (int i = 0; i < allCards.Count; i++)
        {
            if (allCards[i].category == CardBehaviour.CardCategory.Ingredient && ingredients < 5)
            {
                allCards[i].gameObject.SetActive(true);
                ingredients++;
            }
            else if (allCards[i].category == CardBehaviour.CardCategory.Flavor && flavor < 5)
            {
                allCards[i].gameObject.SetActive(true);
                flavor++;
            }
        }
    }
    public void BasicCardUse(CardBehaviour card)
    {
        if (currentDish != null)
        {
           currentDish.AddUp(card); 
        }
    }

    public List<CardBehaviour> ShuffleList(List<CardBehaviour> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = UnityEngine.Random.Range(0, i + 1);
            CardBehaviour temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }
        return list;
    }
}
[Serializable]
public class Dish 
{
    public float salty, sweety, bittery, soury;
    public void AddUp(CardBehaviour card)
    {
        salty += card.addSalt;
        sweety += card.addSweet;
        bittery += card.addBitter;
        soury += card.addSour;
        salty *= card.saltyMult;
        sweety *= card.sweetMult;
        bittery *= card.bitterMult;
        soury *= card.sourMult;
    }
}
