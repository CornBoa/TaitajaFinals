using System;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;

public class CardBehaviour : MonoBehaviour
{
    public enum CardCategory
    {
        Ingredient,Flavor,Equipment,Technique
    }
    //Stat stuff
    public float saltyMult, sweetMult, bitterMult, sourMult;
    public float addSalt, addSweet, addSour, addBitter;
    public Vector3 desiredScale, ogSCale;
    //Main
    public bool draging, inHand,lerping,lerpScaleUp,lerpScaleDown;
    [SerializeField]float lerpTimer,scaleLerpTimer;
    Transform lerpTarget;
    public UnityEvent onUse;
    public CardCategory category;
    
    void Start()
    {
        Hand.instance.PutCardIn(this);
        ogSCale = transform.localScale;
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
        if (lerpScaleUp && !CompareScale())
        {
            scaleLerpTimer += Time.deltaTime;
            transform.localScale = Vector3.Lerp(transform.localScale, desiredScale, scaleLerpTimer);
        }
        else if (lerpScaleUp)
        {
            lerpScaleUp = false;
            scaleLerpTimer= 0;
        }
        else if (lerpScaleDown && !lerpScaleUp && transform.localScale.x > ogSCale.x)
        {
            scaleLerpTimer += Time.deltaTime;
            transform.localScale = Vector3.Lerp(transform.localScale, ogSCale, scaleLerpTimer);
        }
        else if (lerpScaleDown && !lerpScaleUp)
        {
            lerpScaleDown = false;
            scaleLerpTimer = 0;
        }
    }
    bool CompareScale()
    {
        if (transform.localScale.x < desiredScale.x && transform.localScale.z < desiredScale.z && transform.localScale.z < desiredScale.z)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    private void OnMouseEnter()
    {
        Debug.Log("Mouse Entered");
        lerpScaleUp = true;
        lerpScaleDown = false;
    }
    private void OnMouseExit()
    {
        lerpScaleUp = false;
        lerpScaleDown = true;
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
    private void OnDestroy()
    {
        Hand.instance.CardOut(transform.parent);
        onUse.Invoke();
    }
}
