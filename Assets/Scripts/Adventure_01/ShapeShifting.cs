using UnityEngine;

public class ShapeShifting : MonoBehaviour        //变身脚本
{
    [SerializeField] GameObject kun;
    [SerializeField] GameObject peng;
    private void Update()
    {
        if (Input.GetButtonDown("Ult"))
        {
            int energyPoint = this.GetComponent<CharacterInformation>().energyPoint;
            if (energyPoint >= 10 && energyPoint < 40)
            {
                this.GetComponent<CharacterInformation>().energyPoint -= 10;
                GameObject newOj = GameObject.Instantiate(kun, this.transform.position, Quaternion.identity);
                this.transform.parent = newOj.transform;
                this.gameObject.SetActive(false);
            }
            else if (energyPoint == 40)
            {
                this.GetComponent<CharacterInformation>().energyPoint = 0;
                GameObject newOj = GameObject.Instantiate(peng, this.transform.position, Quaternion.identity);
                this.transform.parent = newOj.transform;
                this.gameObject.SetActive(false);
            }
        }
    }
}
