using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    public GameObject target;
    Vector3 distance;
    private void Start()
    {
        if(target==null)
        {
            if(this.gameObject.tag=="SpecialEffects")
            {
                target = GameObject.FindGameObjectWithTag("Player");
            }
        }
        distance = transform.position - target.transform.position;
    }
    private void LateUpdate()
    {
        transform.position = target.transform.position + distance;
    }
}
