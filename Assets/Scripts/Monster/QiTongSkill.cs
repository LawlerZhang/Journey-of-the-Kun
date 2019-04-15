using UnityEngine;

public class QiTongSkill : MonoBehaviour
{
    [SerializeField] GameObject scenario;    //剧本
    [SerializeField] GameObject QiTong_Human;  //人形耆童
    [SerializeField] GameObject darkCloud;
    [SerializeField] GameObject hand; //手
    [SerializeField] GameObject snake;
    float nowSnakeTime = 0.0f;
    [SerializeField] GameObject[] rotatePosition;  //古钟旋转点
    [SerializeField] GameObject[] ancientClocks;  //古钟
    [SerializeField] GameObject[] instruments;   //乐器
    [SerializeField] Vector3 instrumentPosition; //乐器起始位置
    [SerializeField] GameObject[] bloodRains;    //血雨
    int instrumentIndex = 0;    //乐器数组下标
    float instrumentCD = 0.0f;  //当前乐器冷却时间
    int instrumentCount = 0;     //已经释放的乐器数量，当达到8个时，无法再释放
    int residueInstrumentSkill = 3;  //剩余的乐器技能数量
    float bloodRainCD = 0.0f; //当前血雨冷却时间
    bool isCloudArrived = false;  //乌云是否就位
    bool isCloudRemoved = false;  //乌云是否被移除
    int healthPoint;     //血量
    bool canReleaseInstruments = false;  //是否可以释放乐器，75%、50%、25%三个状态时候会触发此技能
    bool isClockBeCalled = false;   //钟是否已经被召唤过
    bool isStarted = false;
    private void Update()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (Vector2.Distance(player.transform.position, transform.position) < 12.0f)
            isStarted = true;
        if (isStarted)
        {
            healthPoint = this.GetComponent<MonsterStatus>().healthPoint;
            if (canReleaseInstruments)
                ReleaseInstruments();
            if (healthPoint >= 67 && healthPoint <= 100)
                CallTheRain();
            else if (healthPoint >= 34 && healthPoint <= 66)
            {
                //释放乐器
                if (residueInstrumentSkill == 3)
                {
                    canReleaseInstruments = true;
                    residueInstrumentSkill--;
                }
                //移除乌云
                if (!isCloudRemoved)
                {
                    foreach (GameObject gameObject in bloodRains)
                        gameObject.SetActive(false);
                    darkCloud.transform.Translate(4.0f * Time.deltaTime, 0, 0);
                    if (darkCloud.transform.position.x >= 161.2f)
                    {
                        Destroy(darkCloud);
                        isCloudRemoved = true;
                    }
                }
                //释放玩乐器，开始召唤古钟
                if (!canReleaseInstruments)
                {
                    CallAncientClocks();
                }
            }
            else if (healthPoint >= 1 && healthPoint <= 33)
            {
                //古钟复位
                ancientClocks[0].GetComponent<AcientClock>().Reset();
                ancientClocks[0].GetComponent<AcientClock>().enabled = false;
                ancientClocks[1].GetComponent<AcientClock>().Reset();
                ancientClocks[1].GetComponent<AcientClock>().enabled = false;
                rotatePosition[0].GetComponent<RotateAncientClock>().enabled = true;
                rotatePosition[1].GetComponent<RotateAncientClock>().enabled = true;
                //释放乐器
                if (residueInstrumentSkill == 2)
                {
                    canReleaseInstruments = true;
                    residueInstrumentSkill--;
                }
                //召唤蛇
                CallSnake();
            }
            else if (healthPoint <= 0)
            {
                scenario.SetActive(true);
                QiTong_Human.SetActive(true);
                Destroy(this.gameObject);
            }
        }
    }
    //释放乐器
    void ReleaseInstruments()
    {
        instrumentCD += Time.deltaTime;
        float needTime = instrumentIndex == 0 ? 2.0f : 0.6f;
        if (instrumentCD >= needTime)
        {
            GameObject.Instantiate(instruments[instrumentIndex], instrumentPosition, Quaternion.identity);
            instrumentIndex = (instrumentIndex + 1) % instruments.Length;
            instrumentCount++;
            instrumentCD = 0.0f;
        }
        if (instrumentCount == 8)
        {
            canReleaseInstruments = false;
            instrumentCount = 0;
        }
    }
    //耆童“血雨”技能
    void CallTheRain()
    {
        if (!isCloudArrived)
        {
            darkCloud.transform.Translate(-4.0f * Time.deltaTime, 0, 0);
            if (darkCloud.transform.position.x <= 143.2f)
            {
                darkCloud.transform.position = new Vector3(143.2f, darkCloud.transform.position.y, darkCloud.transform.position.z);
                isCloudArrived = true;
            }
        }
        else
        {
            bloodRainCD += Time.deltaTime;
            if (bloodRainCD >= 0.5f)
            {
                darkCloud.SetActive(true);
                int rainIndex = Random.Range(0, bloodRains.Length);
                bloodRains[rainIndex].SetActive(true);
                bloodRainCD = 0.0f;
            }
        }
    }
    //召唤钟技能
    void CallAncientClocks()
    {
        if (!isClockBeCalled)
        {
            ancientClocks[0].GetComponent<AcientClock>().enabled = true;
            ancientClocks[0].transform.rotation = Quaternion.identity;
            rotatePosition[0].GetComponent<RotateAncientClock>().enabled = false;
            ancientClocks[1].GetComponent<AcientClock>().enabled = true;
            ancientClocks[1].transform.rotation = Quaternion.identity;
            rotatePosition[1].GetComponent<RotateAncientClock>().enabled = false;
            isClockBeCalled = true;
        }
    }
    //召唤蛇
    void CallSnake()
    {
        nowSnakeTime += Time.deltaTime;
        if (nowSnakeTime >= 5.0f)
        {
            GameObject.Instantiate(snake, new Vector3(145.95f, 0.95f, 0), Quaternion.identity);
            nowSnakeTime = 0.0f;
        }
    }
}
