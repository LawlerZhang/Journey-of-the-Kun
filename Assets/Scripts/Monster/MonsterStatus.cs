using UnityEngine;

public class MonsterStatus : MonoBehaviour
{
    public int healthPoint;        //生命值
    public bool isDie = false;     //是否已经死亡
    public bool isFalled = false;  //在死亡的前提下是否已经坠地
    private void LateUpdate()
    {
        if (healthPoint <= 0)
            isDie = true;
    }
}
