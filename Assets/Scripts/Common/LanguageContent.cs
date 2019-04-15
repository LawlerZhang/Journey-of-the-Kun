using UnityEngine;
using UnityEngine.UI;

public class LanguageContent : MonoBehaviour
{
    [SerializeField] string[] languages;
    private void LateUpdate()
    {
        this.GetComponent<Text>().text = languages[GameInformation.languageIndex];
    }
}
