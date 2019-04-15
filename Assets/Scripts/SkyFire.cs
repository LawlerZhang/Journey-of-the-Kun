using UnityEngine;

public class SkyFire : MonoBehaviour
{
    float nowExistTime = 0.0f;
    private void Update()
    {
        nowExistTime += Time.deltaTime;
        if (nowExistTime >= 5.0f)
            Destroy(gameObject);
    }
    /*private void OnCollisionEnter2D(Collision2D collision)
    {

    }*/
}
