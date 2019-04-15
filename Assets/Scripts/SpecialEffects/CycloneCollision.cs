using UnityEngine;

public class CycloneCollision : MonoBehaviour
{
    float nowTime = 0.0f;
    float moveSpeed = 6.0f;
    AudioSource oldHurtAudioClip;
    private void Awake()
    {
        oldHurtAudioClip = GameObject.Find("AudioSet/OldHurt").GetComponent<AudioSource>();
    }
    private void Update()
    {
        Move();
        DestroySelf();
    }
    private void DestroySelf()
    {
        nowTime += Time.deltaTime;
        if (nowTime >= 2.5f)
            Destroy(this.gameObject);
    }
    private void Move()
    {
        this.transform.Translate(moveSpeed * Time.deltaTime, 0, 0);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Monster")
        {
            collision.gameObject.GetComponent<MonsterStatus>().healthPoint -= 4;
        }
        if (collision.gameObject.name == "QiTong_BeastState")
        {
            oldHurtAudioClip.Play();
            collision.collider.GetComponent<QiTongBehaviour>().isAttacked = true;
            Destroy(gameObject);
        }
        if (collision.gameObject.name == "DiJiang_BeastState")
        {
            collision.collider.GetComponent<DiJiang_BeastState>().isAttacked = true;
            Destroy(gameObject);
        }
    }
}
