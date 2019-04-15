using UnityEngine;

public class PopMenu : MonoBehaviour
{
    [SerializeField] GameObject menu;
    [SerializeField] GameObject selectButton;
    private void Update()
    {
        if (Input.GetButtonDown("Menu"))
        {
            Time.timeScale = 0.0f;
            selectButton.SetActive(true);
            menu.SetActive(true);
        }
    }
}
