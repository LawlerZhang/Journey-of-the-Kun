using UnityEngine;

public class ReturnGame : MonoBehaviour
{
    [SerializeField] GameObject systemSetting;
    [SerializeField] GameObject selectButton;
    [SerializeField] GameObject menu;
    public void Click()
    {
        if (systemSetting != null)
            systemSetting.SetActive(false);
        if (selectButton != null)
            selectButton.SetActive(true);
        if (menu != null)
            menu.SetActive(true);
    }
}
