using UnityEngine;
using UnityEngine.UI;

public class TurnUpDown : MonoBehaviour
{
    [SerializeField] string title;
    [SerializeField] GameObject father;
    public void TurnUp()
    {
        switch(title)
        {
            case "BackgroundMusic":
                {
                    if (father.GetComponent<ChangeValue>().tempBackgroundVolume + 0.05f <= 1)
                        father.GetComponent<ChangeValue>().tempBackgroundVolume += 0.05f;
                    else
                        father.GetComponent<ChangeValue>().tempBackgroundVolume = 1.0f;
                    break;
                }
            case "GameSound":
                {
                    if (father.GetComponent<ChangeValue>().tempGameAudioVolume + 0.05f <= 1)
                        father.GetComponent<ChangeValue>().tempGameAudioVolume += 0.05f;
                    else
                        father.GetComponent<ChangeValue>().tempGameAudioVolume = 1.0f;
                    break;
                }
            case "ScreenSetting":
                {
                    father.GetComponent<ChangeValue>().tempScreenSettingIndex ^= 1;
                    break;
                }
            case "Resolution":
                {
                    father.GetComponent<ChangeValue>().tempResolutionIndex = (father.GetComponent<ChangeValue>().tempResolutionIndex + 1) % 8;
                    break;
                }
            case "Language":
                {
                    father.GetComponent<ChangeValue>().tempLanguageIndex ^= 1;      //语言数组下标在0-1切换
                    break;
                }
            default:break;
        }
    }
    public void TurnDown()
    {
        switch (title)
        {
            case "BackgroundMusic":
                {
                    if (father.GetComponent<ChangeValue>().tempBackgroundVolume - 0.05f >= 0)
                        father.GetComponent<ChangeValue>().tempBackgroundVolume -= 0.05f;
                    else
                        father.GetComponent<ChangeValue>().tempBackgroundVolume = 0.0f;
                    break;
                }
            case "GameSound":
                {
                    if (father.GetComponent<ChangeValue>().tempGameAudioVolume - 0.05f >= 0)
                        father.GetComponent<ChangeValue>().tempGameAudioVolume -= 0.05f;
                    else
                        father.GetComponent<ChangeValue>().tempGameAudioVolume = 0.0f;
                    break;
                }
            case "ScreenSetting":
                {
                    father.GetComponent<ChangeValue>().tempScreenSettingIndex ^= 1;
                    break;
                }
            case "Resolution":
                {
                    father.GetComponent<ChangeValue>().tempResolutionIndex = father.GetComponent<ChangeValue>().tempResolutionIndex - 1 < 0 ? 7 : father.GetComponent<ChangeValue>().tempResolutionIndex - 1;
                    break;
                }
            case "Language":
                {
                    father.GetComponent<ChangeValue>().tempLanguageIndex ^= 1;      //语言数组下标在0-1切换
                    break;
                }
            default: break;
        }
    }
    int Approximate(float value)
    {
        if ((int)value + 0.5f >= (int)value)
            return (int)(value + 0.5f);
        else
            return (int)value;
    }
   
}
