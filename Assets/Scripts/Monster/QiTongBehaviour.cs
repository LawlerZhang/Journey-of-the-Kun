using UnityEngine;

public class QiTongBehaviour : MonoBehaviour
{
    [SerializeField] Color commonColor;
    [SerializeField] Color beAttackedColor;
    [SerializeField] GameObject[] bodyParts;  //耆童身体部位
    public bool isAttacked = false;    //当前是否正在被攻击
    bool shouldBeRed=false;      //是否应该变红
    float redTime = 0.0f;     //当前变红的时间
    private void Update()
    {
        BeAttacked();
    }
    //耆童被攻击后，全身会变红
    void BeAttacked()
    {
        if (isAttacked)
        {
            foreach (GameObject gameObject in bodyParts)
            {
                gameObject.GetComponent<SpriteRenderer>().color = beAttackedColor;
            }
            isAttacked = false;
            shouldBeRed = true;
        }
        else if (shouldBeRed)
        {
            redTime += Time.deltaTime;
            if (redTime >= 0.10f)
            {
                foreach (GameObject gameObject in bodyParts)
                {
                    gameObject.GetComponent<SpriteRenderer>().color = commonColor;
                }
                shouldBeRed = false;
                redTime = 0.0f;
            }
        }
    }
}
