using UnityEngine;

public class ReturnPreHierarchy : MonoBehaviour
{
    [SerializeField] GameObject father;
    [SerializeField] GameObject selectButton;
    public void Click()
    {
        selectButton.SetActive(true);
        father.SetActive(false);
    }
}
