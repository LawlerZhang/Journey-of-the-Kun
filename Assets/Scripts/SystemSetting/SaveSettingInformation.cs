using UnityEngine;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveSettingInformation : MonoBehaviour
{
    [SerializeField] GameObject backgroundMusic;
    [SerializeField] GameObject gameSound;
    [SerializeField] GameObject screenSetting;
    [SerializeField] GameObject resolution;
    [SerializeField] GameObject language;
    [SerializeField] GameObject audioInitialize;
    Resolution maxResolution;    //设备的最大分辨率
    int[] resolutions = new int[16];
    SettingInformation settingInformation = new SettingInformation();
    private void Awake()
    {
        //加载所支持的设备分辨率
        maxResolution = Screen.resolutions[Screen.resolutions.Length - 1];
        for (int i = 0; i < 15; i += 2)
        {
            resolutions[i] = (int)((i / 2 + 1) * 0.125f * maxResolution.width);
            resolutions[i + 1] = (int)((i / 2 + 1) * 0.125f * maxResolution.height);
        }
    }
    public void Click()
    {
        GameInformation.backgroundVolume = backgroundMusic.GetComponent<ChangeValue>().tempBackgroundVolume;
        GameInformation.gameAudioVolume = gameSound.GetComponent<ChangeValue>().tempGameAudioVolume;
        GameInformation.screenSettingIndex = screenSetting.GetComponent<ChangeValue>().tempScreenSettingIndex;
        GameInformation.resolutionIndex = resolution.GetComponent<ChangeValue>().tempResolutionIndex;
        GameInformation.languageIndex = language.GetComponent<ChangeValue>().tempLanguageIndex;
        //改变屏幕分辨率
        Screen.SetResolution(resolutions[GameInformation.resolutionIndex * 2], resolutions[GameInformation.resolutionIndex * 2 + 1], GameInformation.screenSettingIndex == 0 ? false : true);

        Save();
    }
    void Save()
    {
        settingInformation.backgroundVolume = GameInformation.backgroundVolume;
        settingInformation.gameAudioVolume = GameInformation.gameAudioVolume;
        settingInformation.screenSettingIndex = GameInformation.screenSettingIndex;
        settingInformation.resolutionIndex = GameInformation.resolutionIndex;
        settingInformation.languageIndex = GameInformation.languageIndex;
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = File.Create(Application.dataPath + "/Information" + "/SettingInformation.txt");
        bf.Serialize(fs, settingInformation);
        fs.Close();
        audioInitialize.GetComponent<AudioInitialize>().Start();

        Debug.Log("保存成功");
    }
}
