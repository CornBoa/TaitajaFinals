using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Dish currentDish;
    public List<CardBehaviour> allCards;
    [SerializeField] GameObject RerollButton;
    [SerializeField] Transform boxSpawn,cupBOardSpawn;
    [SerializeField] UnityEvent onFirstTurnDealt,onSecondTurnDealt,onThirdTurnDealt;
    [SerializeField] Hand ing;
    bool rerolling = false,rolledTooMuch = false;
    private void Awake()
    {
        instance = this;
    }
    [ContextMenu("ShuffleRQ")]
    public void FirstDealCards()
    {
        allCards = ShuffleList(allCards);
        StartCoroutine(DealCardsFirstTurn());
    }
    IEnumerator DealCardsFirstTurn()
    {
        if (!rolledTooMuch)
        {
            if (ing.cards.Count > 0) foreach (var card in ing.cards)
            {
               card.gameObject.SetActive(false);
            }
            ing.cards.Clear();
            int ingredients = 0;
            int flavor = 0;
            int cardAmount;
            if (rerolling) cardAmount = 2;
            else cardAmount = 3;
            for (int i = 0; i < allCards.Count; i++)
            {
                if (allCards[i].category == CardBehaviour.CardCategory.Ingredient && ingredients < cardAmount)
                {                    
                    ing.PutCardIn(allCards[i]);
                    allCards[i].transform.position = boxSpawn.position;
                    allCards[i].gameObject.SetActive(true);
                    ingredients++;
                    yield return new WaitForSeconds(0.25f);
                }
                else if (allCards[i].category == CardBehaviour.CardCategory.Flavor && flavor < cardAmount)
                { 
                    ing.PutCardIn(allCards[i]);
                    allCards[i].lerping = true;
                    allCards[i].transform.position = boxSpawn.position;
                    allCards[i].gameObject.SetActive(true);
                    flavor++;
                    yield return new WaitForSeconds(0.25f);
                }
            }
            if (!rerolling)
            {
                
                rerolling = true;
            }
            else RerollButton.SetActive(false);
            onFirstTurnDealt.Invoke();
        }
    }
    [ContextMenu("SécondTurn")]
    public void SecondtDealCards()
    {
        allCards = ShuffleList(allCards);
        StartCoroutine(DealCardsSecondTurn());
    }
    IEnumerator DealCardsSecondTurn()
    {
        if (!rolledTooMuch)
        {
            if (ing.cards.Count > 0) foreach (var card in ing.cards)
            {
                 card.gameObject.SetActive(false);
            }
            ing.cards.Clear();
            int equipment = 0;
            int cardAmount;
            if (rerolling) cardAmount = 2;
            else cardAmount = 3;
            for (int i = 0; i < allCards.Count; i++)
            {
                if (allCards[i].category == CardBehaviour.CardCategory.Ingredient && equipment < cardAmount)
                {
                    ing.PutCardIn(allCards[i]);
                    allCards[i].transform.position = boxSpawn.position;
                    allCards[i].gameObject.SetActive(true);
                    equipment++;
                    yield return new WaitForSeconds(0.25f);
                }
            }
            if (!rerolling)
            {

                rerolling = true;
            }
            else RerollButton.SetActive(false);
            onFirstTurnDealt.Invoke();
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
