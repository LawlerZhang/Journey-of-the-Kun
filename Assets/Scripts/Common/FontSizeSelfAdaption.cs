using UnityEngine;
using UnityEngine.UI;

public class FontSizeSelfAdaption : MonoBehaviour
{
    int nowSize;
    private void Awake()
    {
        nowSize = GetComponent<Text>().fontSize;
    }
    private void FixedUpdate()
    {
        int size1 = (int)Mathf.Ceil(nowSize / 1920.0f * Screen.width);
        int size2 = GetComponent<Text>().fontSize = (int)Mathf.Ceil(nowSize / 1080.0f * Screen.height);
        GetComponent<Text>().fontSize = size1 <= size2 ? size1 : size2;
    }
}
