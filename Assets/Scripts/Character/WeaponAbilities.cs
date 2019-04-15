using UnityEngine;
using UnityEngine.UI;

public class WeaponAbilities : MonoBehaviour
{
    [SerializeField] AudioSource throwDart;
    [SerializeField] GameObject dart;
    [SerializeField] Vector3 deltaPos;
    [SerializeField] GameObject actionBar;       //人物动作条
    public float CD = 0.0f;
    GameObject player;
    float actionBarSize = 0.0f;      //动作条大小
    Animator animator;
    Vector3 startPoint;
    float nowCD = 0.0f;       //当前冷却时间
    private void Awake()
    {
        player = GameObject.Find("Girl");
        animator = this.GetComponent<Animator>();
    }
    private void Update()
    {
        nowCD += Time.deltaTime;
        ThrowDarts();
    }
    void ThrowDarts()
    {
        if (nowCD >= CD)
        {
            if (Input.GetButton("Fire1"))
            {
                if (actionBarSize >= 0.1f)
                    actionBar.SetActive(true);
                actionBarSize += Time.deltaTime;
                if (actionBarSize >= 1.0f)
                    actionBarSize = 1.0f;
                player.GetComponent<CharacterAction>().enabled = false;
            }
            if (Input.GetButtonUp("Fire1"))
            {
                nowCD = 0.0f;
                InitializeDart();
                animator.SetTrigger("ThrowForward");
                actionBarSize = 0.0f;
                actionBar.SetActive(false);
                player.GetComponent<CharacterAction>().enabled = true;
            }
            actionBar.GetComponent<Scrollbar>().size = actionBarSize;
        }
    }
    void InitializeDart()
    {
        int direction = 1;
        direction = GetComponent<CharacterAction>().direction;
        startPoint = transform.position + new Vector3(direction * deltaPos.x, deltaPos.y, deltaPos.z);
        throwDart.Play();
        GameObject newDart = GameObject.Instantiate(dart, startPoint, Quaternion.identity);
        newDart.GetComponent<DartBehaviour>().actionBarSize = actionBarSize;
    }
}
