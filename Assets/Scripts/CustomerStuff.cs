using TMPro;
using UnityEngine;

public class CustomerStuff : MonoBehaviour
{
    public enum TastePref
    {
        Salty,Sweet,Bitter,Sour,PourBalance
    }
    public TextMeshProUGUI orderText;
    public TastePref tastepreferance;
    private void Start()
    {
        
    }
    public void RandomOrder()
    {
        tastepreferance = (TastePref)Random.Range(0, 3);
        switch (tastepreferance)
        {
            case TastePref.Sweet:
                orderText.text = "I would like something sweet";
                break;
            case TastePref.Bitter:
                orderText.text = "I would like something bitter";
                break;
            case TastePref.Sour:
                orderText.text = "I would like something sour";
                break;
            case TastePref.Salty:
                orderText.text = "I would like something salty";
                break;
        }
    }
}
