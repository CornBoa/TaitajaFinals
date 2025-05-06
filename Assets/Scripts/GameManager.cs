using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Dish currentDish;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
[Serializable]
public class Dish 
{
    public float salty, sweety, bittery, soury;
}
