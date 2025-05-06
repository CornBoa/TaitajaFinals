using System;
using System.Threading;
using UnityEngine;

public class CardBehaviour : MonoBehaviour
{
    public enum CardCategory
    {
        Ingredient,Flavor,Equipment,Technique
    }
    //Stat stuff
    public float saltyMult, sweetMult, bitterMult, sourMult;
    public float addSalt, addSweet, addSour, addBitter;
    public bool draging, inHand,lerping;
    [SerializeField]float lerpTimer;
    Transform lerpTarget;
    void Start()
    {
        Hand.instance.PutCardIn(this);
    }
    void Update()
    {
        if (lerping && Vector3.Distance(transform.position, lerpTarget.position) > 0.01)
        {
            lerpTimer += Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position,lerpTarget.position,lerpTimer);
        }
        else if (lerping)
        {
            lerping = false;
            lerpTimer = 0;
        }
    }
    private void OnMouseDown()
    {
        if (transform.parent != null) Hand.instance.CardOut(transform.parent);
        Vector3 newPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        GameObject.FindGameObjectWithTag("Anchor").transform.position = newPos;
        transform.parent = GameObject.FindGameObjectWithTag("Anchor").transform;
        draging = true;
    }
    private void OnMouseDrag()
    {
        Vector3 newPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.parent.position = new Vector3(newPos.x, newPos.y, 0);
    }
    private void OnMouseUp()
    {
        draging = false;
        transform.parent = null;
        Hand.instance.PutCardIn(this);
    }
    public void LerpTo(Transform slot)
    {
        transform.parent = slot;
        lerpTarget = slot;
        lerping = true;
    }
}
