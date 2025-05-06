using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public static Hand instance;
    public List<Transform> cardSlots;
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
                card.LerpTo(cardSlots[i].transform);
                break;
            }
        }
    }
    public void CardOut(Transform slot)
    {
        slot.gameObject.SetActive(false);
    }
    void Update()
    {
        
    }
}
