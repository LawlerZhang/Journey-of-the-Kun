using UnityEngine;

public class FlyClaw : MonoBehaviour
{
    [SerializeField] GameObject noumenon;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag=="Weapon")
        {
            int damage = GameInformation.ATK;
            noumenon.GetComponent<MonsterStatus>().healthPoint -= damage;
            noumenon.GetComponent<QiTongBehaviour>().isAttacked = true;
            GameObject.Find("AudioSet/SnakeBlood").GetComponent<AudioSource>().Play();
        }
    }
}
