using UnityEngine;
using UnityEngine.UI;

public class SaveTree : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject rescueAnnulus;
    [SerializeField] Text rescueRate;
    [SerializeField] GameObject[] trees;
    bool[] isRescued;
    [SerializeField] Material beSavedMaterial;   //被解救后的材质
    int treeIndex = -1;     //树数组下标
    float rescueSpeed = 0.4f;    //每秒钟能完成40%
    private void Awake()
    {
        isRescued = new bool[9];
        for (int i = 0; i <= 8; i++)
            isRescued[i] = false;
    }
    private void Update()
    {
        if (player == null || !player.activeInHierarchy)
            player = GameObject.FindWithTag("Player");
        if (treeIndex != -1 && !isRescued[treeIndex])
            Rescue();
        if (treeIndex == -1)
        {
            rescueAnnulus.GetComponent<Image>().fillAmount = 0;
            rescueAnnulus.SetActive(false);
        }
        CaculateTreeIndex();
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
        //解救成功
        if (rescueAnnulus.GetComponent<Image>().fillAmount == 1)
        {
            trees[treeIndex].GetComponent<SpriteRenderer>().material = beSavedMaterial;
            trees[treeIndex].GetComponent<Collider2D>().enabled = true;
            trees[treeIndex].GetComponent<GenerateSnakes>().enabled = false;
            if (trees[treeIndex].transform.childCount > 0)
                trees[treeIndex].transform.GetChild(0).gameObject.SetActive(true);
            rescueAnnulus.GetComponent<Image>().fillAmount = 0;
            rescueAnnulus.SetActive(false);
            isRescued[treeIndex] = true;
        }
    }
    void CaculateTreeIndex()
    {
        float posX = player.transform.position.x;
        if (Mathf.Abs(14.34f - posX) <= 1.0f)
            treeIndex = 0;
        else if (Mathf.Abs(27.51f - posX) <= 1.0f)
            treeIndex = 1;
        else if (Mathf.Abs(44.74f - posX) <= 1.0f)
            treeIndex = 2;
        else if (Mathf.Abs(69.05f - posX) <= 1.0f)
            treeIndex = 3;
        else if (Mathf.Abs(85.2f - posX) <= 1.0f)
            treeIndex = 4;
        else if (Mathf.Abs(99.06f - posX) <= 1.0f)
            treeIndex = 5;
        else if (Mathf.Abs(196.87f - posX) <= 1.0f)
            treeIndex = 6;
        else if (Mathf.Abs(253.0f - posX) <= 1.0f)
            treeIndex = 7;
        else if (Mathf.Abs(274.89f - posX) <= 1.0f)
            treeIndex = 8;
        else
            treeIndex = -1;
    }
}
