using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] GameObject target;
    float deltaX;
   
    private void Awake()
    {
        deltaX = transform.position.x - target.transform.position.x;
       
    }
    private void LateUpdate()
    {
        if (target == null || !target.activeInHierarchy)
        {
            target = GameObject.FindGameObjectWithTag("Player");
        }
        this.transform.position = new Vector3(deltaX + target.transform.position.x, transform.position.y, transform.position.z);
    }
}
