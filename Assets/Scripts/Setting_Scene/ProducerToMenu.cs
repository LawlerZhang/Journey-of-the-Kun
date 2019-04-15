using UnityEngine;

public class ProducerToMenu : MonoBehaviour
{
    [SerializeField] GameObject menu;
    [SerializeField] GameObject producer;
    [SerializeField] GameObject selectButton;
    public void Click()
    {
        menu.SetActive(true);
        producer.SetActive(false);
        selectButton.SetActive(true);
        selectButton.GetComponent<SelectButton>().authority = 0;
    }
}
