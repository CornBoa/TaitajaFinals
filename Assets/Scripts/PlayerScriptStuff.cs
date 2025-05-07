using UnityEngine;
using UnityEngine.Events;

public class PlayerScriptStuff : MonoBehaviour
{
    Animator animator;
    public UnityEvent one,two,three,four;
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    
    public void StartTurnOne()
    {
        one.Invoke();
    }
    public void StartTurnTwo()
    {
        two.Invoke();
    }
    public void StartTurnThree()
    {
        three.Invoke(); 
    }
    public void StartTurnFour()
    {
        four.Invoke();
    }
}
