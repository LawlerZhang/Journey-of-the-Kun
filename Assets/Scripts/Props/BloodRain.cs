using UnityEngine;
//此脚本挂在雨滴上
public class BloodRain : MonoBehaviour
{
    Vector3 startPosition;
    private void Awake()
    {
        startPosition = this.transform.position;
    }
    void Update()
    {
        if (this.transform.position.y < -10.0f)
        {
            this.transform.position = startPosition;
            this.gameObject.SetActive(false);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            this.transform.position = startPosition;
            this.gameObject.SetActive(false);
        }
    }
}
