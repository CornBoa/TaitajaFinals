using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public static Hand instance;
    public List<Transform> cardSlots;
    public List<CardBehaviour> cards;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        
    }
    public void PutCardIn(CardBehaviour card)
    {
        for (int i = 0; i < cardSlots.Count; i++)
        {
            if (!cardSlots[i].gameObject.activeSelf)
            {
                cardSlots[i].gameObject.SetActive(true);
                cards.Add(card);
                card.LerpTo(cardSlots[i].transform);
                Debug.Log(cardSlots[i]);
                break;
            }
        }
    }
    public void CardOut(Transform slot,CardBehaviour card)
    {
        card.transform.parent = null;
        slot.gameObject.SetActive(false);
        cards.Remove(card);
    }
    void Update()
    {
        
    }
}
