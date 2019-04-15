using UnityEngine;
using UnityEngine.UI;

public class Heath_EnergyController : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject hpText;
    [SerializeField] GameObject youDie;
    [SerializeField] GameObject[] energyBars;
    [SerializeField] Color[] nowColor;
    AudioSource failGameAudioClip;
    AudioSource backgoundAudioClip;
    int nowColorIndex = 0;
    int energyPoint;
    bool isDead = false;     //标志是否死亡，主要用于避免声音重复播放
    float nowTime = 0.0f;
    private void Awake()
    {
        failGameAudioClip = GameObject.Find("AudioSet/FailGame").GetComponent<AudioSource>();
        backgoundAudioClip = GameObject.Find("AudioSet/Background").GetComponent<AudioSource>();
    }
    private void Start()
    {
        energyPoint = player.GetComponent<CharacterInformation>().energyPoint;
        hpText.GetComponent<Text>().text = "HP." + player.GetComponent<CharacterInformation>().healthPoint.ToString();
    }
    private void Update()
    {
        energyPoint = player.GetComponent<CharacterInformation>().energyPoint;
        ShowEnergy();
        ShowHeathPoint();
        if (energyPoint == 40)
            TwinkleBars(0.2f);
    }
    void ShowEnergy()
    {
        int barCount = 0;
        if (energyPoint == 0) barCount = 0;
        else if (energyPoint > 0 && energyPoint <= 10) barCount = 1;
        else if (energyPoint > 10 && energyPoint <= 20) barCount = 2;
        else if (energyPoint > 20 && energyPoint <= 30) barCount = 3;
        else if (energyPoint > 30 && energyPoint <= 40) barCount = 4;
        for (int i = 0; i < barCount; i++) energyBars[i].SetActive(true);
        for (int i = 3; i >= barCount; i--) energyBars[i].SetActive(false);
        for (int i = 0; i < energyPoint / 10; i++) energyBars[i].transform.GetComponent<Scrollbar>().size = 1;
        if (energyPoint % 10 != 0) energyBars[barCount - 1].GetComponent<Scrollbar>().size = (float)(energyPoint - (barCount - 1) * 10) / (float)10;
    }
    void ShowHeathPoint()
    {
        int healthPoint = player.GetComponent<CharacterInformation>().healthPoint;
        if (healthPoint > 0)
            hpText.GetComponent<Text>().text = "HP." + healthPoint.ToString();
        else if (healthPoint == 0 )
        {
            backgoundAudioClip.Stop();
            hpText.GetComponent<Text>().text = "死亡";
            player.GetComponent<Collider2D>().enabled = false;
            player.GetComponent<CharacterAction>().enabled = false;
            player.GetComponent<CharacterCollision>().enabled = false;
            player.GetComponent<Animator>().enabled = false;
            if (player.transform.position.y <= -10.0f && !isDead)
            {
                isDead = true;
                failGameAudioClip.Play();
                youDie.SetActive(true);
                Time.timeScale = 0.0f;
            }

        }
       /* if (player.transform.position.y <= -10.0f && isDead)
        {
            failGameAudioClip.Play();
            youDie.SetActive(true);
            Time.timeScale = 0.0f;
        }*/
    }
    void  TwinkleBars(float intervalTime)
    {
        nowTime += Time.deltaTime;
        if (nowTime >= intervalTime)
        {
            nowColorIndex ^= 1;
            foreach(GameObject gameobject in energyBars)
            {
                gameobject.transform.GetChild(0).GetComponent<Image>().color = nowColor[nowColorIndex];
            }
            nowTime = 0.0f;
        }
    }
}
