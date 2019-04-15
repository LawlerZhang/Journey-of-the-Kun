using UnityEngine;

public class GenerateSnakes : MonoBehaviour
{
    [SerializeField] GameObject[] snakes;
    [SerializeField] GameObject controller;
    [SerializeField] string snakeColor;
    [SerializeField] GameObject keyTip;    //屏幕上显示的按键提示
    GameObject player;
    int snakeIndex;        //蛇数组下标，0代表绿蛇，1代表粉蛇
    float nowGenerateTime = 8.0f;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (snakeColor == "Green")
            snakeIndex = 0;
        else if (snakeColor == "Pink")
            snakeIndex = 1;
    }
    private void Update()
    {
        if (player == null || !player.activeInHierarchy)
            player = GameObject.FindGameObjectWithTag("Player");
        Genetate();
        SaveTree();
    }
    private void OnDisable()
    {
        if (gameObject != null)
        {
            if (keyTip)
            {
                if (keyTip != null)
                    keyTip.SetActive(false);
                if (controller != null)
                    controller.SetActive(false);
            }
        }
    }
    //产生小蛇
    void Genetate()
    {
        if (Mathf.Abs(transform.position.x - player.transform.position.x) <= 15.0f)
        {
            nowGenerateTime += Time.deltaTime;
            if (nowGenerateTime >= 8.0f)
            {
                Vector3 newPos = this.transform.position + new Vector3(-0.4f, -1.5f, 0);
                GameObject.Instantiate(snakes[snakeIndex], newPos, Quaternion.identity);
                nowGenerateTime = 0.0f;
            }
        }
    }
    //解救灵魂树
    void SaveTree()
    {
        float distanceX = Mathf.Abs(transform.position.x - player.transform.position.x);
        float distanceY = Mathf.Abs(transform.position.y - player.transform.position.y);
        if (distanceX <= 1.0f && distanceY <= 2.0f)
        {
            keyTip.SetActive(true);
            controller.SetActive(true);
        }
        else if (distanceX > 1.0f && distanceX < 5.0f)
        {
            keyTip.SetActive(false);
            controller.SetActive(false);
        }
    }
}
