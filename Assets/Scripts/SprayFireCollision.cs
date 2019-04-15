using UnityEngine;

public class SprayFireCollision : MonoBehaviour
{
    float nowTime = 0.0f;
    private void Update()
    {
        DestroySelf();
    }
    private void DestroySelf()
    {
        nowTime += Time.deltaTime;
        if (nowTime >= 1.33f)
            Destroy(this.gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag=="Monster")
        {
            collision.gameObject.GetComponent<MonsterStatus>().healthPoint--;
        }
    }
}
