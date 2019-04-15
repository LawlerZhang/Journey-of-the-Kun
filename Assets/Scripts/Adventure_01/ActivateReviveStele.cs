using UnityEngine;
using UnityEngine.UI;

public class ActivateReviveStele : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject rescueAnnulus;
    [SerializeField] Text rescueRate;
    [SerializeField] GameObject[] steles;
    bool[] isRescued;
    int steleIndex = -1;     //树数组下标
    float rescueSpeed = 0.4f;    //每秒钟能完成40%
    private void Awake()
    {
        isRescued = new bool[4];
        for (int i = 0; i <= 3; i++)
            isRescued[i] = false;
    }
    private void Update()
    {
        if (player == null || !player.activeInHierarchy)
            player = GameObject.FindWithTag("Player");
        if (steleIndex != -1 && !isRescued[steleIndex])
            Rescue();
        if (steleIndex == -1)
        {
            rescueAnnulus.GetComponent<Image>().fillAmount = 0;
            rescueAnnulus.SetActive(false);
        }
        CaculateSteleIndex();
    }
    void Rescue()
    {
        //开始解救
        if (Input.GetButton("Rescue"))
        {
            rescueAnnulus.SetActive(true);
            float increment = rescueSpeed * Time.deltaTime;
            if (rescueAnnulus.GetComponent<Image>().fillAmount + increment <= 1.0)
                rescueAnnulus.GetComponent<Image>().fillAmount += increment;
            else
                rescueAnnulus.GetComponent<Image>().fillAmount = 1;
        }
        //圆环消失
        if (Input.GetButtonUp("Rescue"))
        {
            rescueAnnulus.GetComponent<Image>().fillAmount = 0;
            rescueAnnulus.SetActive(false);
        }
        rescueRate.text = ((int)(rescueAnnulus.GetComponent<Image>().fillAmount * 100)).ToString() + "%";
        //激活成功
        if (rescueAnnulus.GetComponent<Image>().fillAmount == 1)
        {
            GameInformation.nowRevivePostionX = steles[steleIndex].transform.position.x;
            rescueAnnulus.GetComponent<Image>().fillAmount = 0;
            steles[steleIndex].GetComponent<ShowKeyTip>().enabled = false;
            rescueAnnulus.SetActive(false);
            isRescued[steleIndex] = true;
        }
    }
    void CaculateSteleIndex()
    {
        float posX = player.transform.position.x;
        if (Mathf.Abs(55.91f - posX) <= 1.0f)
            steleIndex = 0;
        else if (Mathf.Abs(103.8f - posX) <= 1.0f)
            steleIndex = 1;
        else if (Mathf.Abs(212.25f - posX) <= 1.0f)
            steleIndex = 2;
        else if (Mathf.Abs(283.6f - posX) <= 1.0f)
            steleIndex = 3;
        else
            steleIndex = -1;
    }
}

