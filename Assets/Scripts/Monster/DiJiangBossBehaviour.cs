using UnityEngine;

public class DiJiangBossBehaviour : MonoBehaviour
{
    [SerializeField] GameObject scenario;
    bool canInvoke = true;
    bool canAct = false;
    [SerializeField] GameObject daDiJiang;
    GameObject mainCamera;
    private void Awake()
    {
        mainCamera = GameObject.Find("Main Camera");
    }
    private void Update()
    {
        if (GameScreen.IsInScreen(this.gameObject, mainCamera))
        {
            canAct = true;
        }
        if (canAct)
        {
            if (canInvoke)
            {
                Invoke("DestroySelf", 2.0f);
                canInvoke = false;
            }
        }
    }
    void DestroySelf()
    {
        scenario.SetActive(true);
        daDiJiang.SetActive(true);
        Destroy(this.gameObject);
    }
}
