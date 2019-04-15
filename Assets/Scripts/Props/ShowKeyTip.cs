using UnityEngine;

public class ShowKeyTip : MonoBehaviour
{
    GameObject player;
    [SerializeField] GameObject keyTip;
    [SerializeField] int wantedIndex;   //当显示按键引导时候，希望看到的提示的数组的下标
    [SerializeField] GameObject controller;
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        if (player == null || !player.activeInHierarchy)
            player = GameObject.FindGameObjectWithTag("Player");
        Show();
    }
    private void OnDisable()
    {
        if (gameObject != null)
        {
            if (keyTip)
            {
                keyTip.SetActive(false);
                controller.SetActive(false);
            }
        }
    }
    void Show()
    {
        float distanceX = Mathf.Abs(transform.position.x - player.transform.position.x);
        float distanceY = Mathf.Abs(transform.position.y - player.transform.position.y);
        if (distanceX <= 1.0f && distanceY <= 2.0f)
        {
            keyTip.SetActive(true);
            controller.SetActive(true);
            keyTip.GetComponent<KeyTip>().keyTipIndex = wantedIndex;
        }
        else if (distanceX > 1.0f && distanceX < 5.0f)
        {
            keyTip.SetActive(false);
            controller.SetActive(false);
        }
    }
}
