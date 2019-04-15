using UnityEngine;
using UnityEngine.UI;

public class Scenario : MonoBehaviour
{
    string[] scenarios;
    int nowScenarioIndex = 0;
    bool shouldOpenTimer = false;   //开始计时一段时间文本消失
    float nowTime = 0.0f;
    private void Awake()
    {
        scenarios = new string[2];
        scenarios[0] = "耆童：“没想到有两下子，先撤了”";
        scenarios[1] = "帝江：“你竟然敢来挑战我”";
    }
    private void OnEnable()
    {
        GetComponent<Text>().text = scenarios[nowScenarioIndex];
        shouldOpenTimer = true;
        nowScenarioIndex++;
    }
    private void Update()
    {
        if (shouldOpenTimer)
            Timer();
    }
    //计时
    void Timer()
    {
        nowTime += Time.deltaTime;
        if (nowTime >= 4.0f)
        {
            shouldOpenTimer = false;
            gameObject.SetActive(false);
        }
    }
}
