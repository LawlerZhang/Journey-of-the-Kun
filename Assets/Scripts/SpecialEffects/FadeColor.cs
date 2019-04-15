using UnityEngine;
using UnityEngine.UI;

public class FadeColor : MonoBehaviour
{
    [SerializeField] float fadeSpeed;
    [SerializeField] Color a;
    [SerializeField] Color b;
    float nowValue = 0.0f;
    private void Update()
    {
        Fade();
    }
    void Fade()
    {
        nowValue += Time.deltaTime * fadeSpeed;
        GetComponent<Image>().color = Color.Lerp(a, b, nowValue);
        if(nowValue>=1.0f)
        { 
            switch (this.gameObject.name)
            {
                case "SelectMask":
                    {
                        nowValue = 0.0f;
                        SelectButton.isFaded = true;
                        gameObject.GetComponent<FadeColor>().enabled = false;
                        break;
                    }
                case "CommonMask":
                    {
                        nowValue = 0.0f;
                        gameObject.SetActive(false);
                        break;
                    }
            }
          
           
        }
    }
}
