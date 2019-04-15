using UnityEngine;

public class LiquidMedicine : MonoBehaviour
{
    [SerializeField] Color[] color;
    [SerializeField] GameObject player;
    int index;
    private void Awake()
    {
        index = Random.Range(0, 3);
    }
    void Start()
    {
        GetComponent<SpriteRenderer>().color = color[index];
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.name == "Girl")
        {
            if (index == 0)
            {
                player.GetComponent<CharacterInformation>().healthPoint++;
            }
            else if (index == 1)
            {
                GameInformation.ATK += 2;
            }
            else if (index == 1)
            {
                player.GetComponent<WeaponAbilities>().CD *= 0.3f;
            }
            Destroy(this.gameObject);
        }
    }
}
