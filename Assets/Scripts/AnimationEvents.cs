using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    Animator animator;
    void Awake()
    {
        animator = this.GetComponent<Animator>();
    }
	public void TriggerIdle()
    {
        animator.SetTrigger("Idle");
    }
}
