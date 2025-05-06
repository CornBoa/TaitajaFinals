using UnityEngine;

public class DropLocation : MonoBehaviour
{
    public static DropLocation Instance;
    [SerializeField]LayerMask layerMask;
    CardBehaviour card;
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Card entered");
        if (other.GetComponent<CardBehaviour>() != null)
        {
            card = other.GetComponent<CardBehaviour>();
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<CardBehaviour>() != null)
        {
            card = null;
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (card != null&& !card.draging)
        {
            CheckCards();
        }
    }

    public void CheckCards()
    {
            Destroy(card.gameObject);
            Debug.Log("Did stuff");
    }
}
