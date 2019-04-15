using UnityEngine;
using UnityEngine.UI;

public class ChangeValue : MonoBehaviour
{
    [SerializeField] GameObject value;
    public float tempBackgroundVolume;
    public float tempGameAudioVolume;
    public int tempScreenSettingIndex = 0;
    Resolution maxResolution;    //设备的最大分辨率
    int[] resolutions = new int[16];
    public int tempResolutionIndex;
    string[] screenSettingOptions = { "窗口", "全屏", "Windowed", "FullScreen" };
    public int tempLanguageIndex;
    string[] languages = new string[2] { "简体中文", "English" };
    private void Start()
    {
        tempBackgroundVolume = GameInformation.backgroundVolume;
        tempGameAudioVolume = GameInformation.gameAudioVolume;
        tempScreenSettingIndex = GameInformation.screenSettingIndex;
        tempResolutionIndex = GameInformation.resolutionIndex;
        tempLanguageIndex = GameInformation.languageIndex;
    }
    private void OnEnable()
    {
        Start();
    }
    void Update()
    {
        switch (this.gameObject.name)
        {
            case "BackgroundMusic":
                {
                    value.GetComponent<Text>().text = (Approximate(tempBackgroundVolume * 100)).ToString() + "%";
                    break;
                }
            case "GameSound":
                {
                    value.GetComponent<Text>().text = (Approximate(tempGameAudioVolume * 100)).ToString() + "%";
                    break;
                }
            case "ScreenSetting":
                {
                    value.GetComponent<Text>().text = screenSettingOptions[GameInformation.languageIndex * 2 + tempScreenSettingIndex];
                    break;
                }
            case "Resolution":
                {
                    maxResolution = Screen.resolutions[Screen.resolutions.Length - 1];
                    for (int i = 0; i < 15; i += 2)
                    {
                        resolutions[i] = (int)((i / 2 + 1) * 0.125f * maxResolution.width);
                        resolutions[i + 1] = (int)((i / 2 + 1) * 0.125f * maxResolution.height);
                    }
                    value.GetComponent<Text>().text = resolutions[tempResolutionIndex * 2].ToString() + " × " + resolutions[tempResolutionIndex * 2 + 1].ToString();
                    break;
                }
            case "Language":
                {
                    value.GetComponent<Text>().text = languages[tempLanguageIndex];
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
