using UnityEngine;

public class GenerateDiJiang : MonoBehaviour
{
    [SerializeField] GameObject diJiang;
    GameObject player;
    int DiJiangCount = 0;
    bool canInvoke = true;
    bool isTheFirstInitialize = true;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        if (player == null || !player.activeInHierarchy)
            player = GameObject.FindGameObjectWithTag("Player");
        if (isTheFirstInitialize)
        {
            Initialize();
            isTheFirstInitialize = false;
        }
        if (canInvoke && DiJiangCount < 4)
        {
            canInvoke = false;
            Invoke("Initialize", 8.0f);
        }
    }
    private void Initialize()
    {
        if (Mathf.Abs(this.transform.position.x - player.transform.position.x) <= 15.0f)
        {
            GameObject.Instantiate(diJiang, this.transform.position, this.transform.rotation);
            DiJiangCount++;
        }
        canInvoke = true;
    }
}
