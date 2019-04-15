using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class LoadSettingInformation : MonoBehaviour
{
    SettingInformation settingInformation;
    Resolution maxResolution;    //设备的最大分辨率
    int[] resolutions = new int[16];
    private void Awake()
    {
        //加载所支持的设备分辨率
        maxResolution = Screen.resolutions[Screen.resolutions.Length - 1];
        for (int i = 0; i < 15; i += 2)
        {
            resolutions[i] = (int)((i / 2 + 1) * 0.125f * maxResolution.width);
            resolutions[i + 1] = (int)((i / 2 + 1) * 0.125f * maxResolution.height);
        }
        //加载设置文件信息
        settingInformation = new SettingInformation();
        if(Load())
        {
            GameInformation.backgroundVolume = settingInformation.backgroundVolume;
            GameInformation.gameAudioVolume = settingInformation.gameAudioVolume;
            GameInformation.screenSettingIndex = settingInformation.screenSettingIndex;
            GameInformation.resolutionIndex = settingInformation.resolutionIndex;
            GameInformation.languageIndex = settingInformation.languageIndex;
        }
        else 
        {
            GameInformation.backgroundVolume = 1.0f;
            GameInformation.gameAudioVolume = 1.0f;
            GameInformation.screenSettingIndex = 0;
            GameInformation.resolutionIndex = 7;
            GameInformation.languageIndex = 0;
        }
        //屏幕分辨率初始化
        Screen.SetResolution(resolutions[GameInformation.resolutionIndex * 2], resolutions[GameInformation.resolutionIndex * 2 + 1], GameInformation.screenSettingIndex == 0 ? false : true);
    }
    bool Load()
    {
        if (File.Exists(Application.dataPath + "/Information" + "/SettingInformation.txt"))
        {
            //反序列化过程
            //创建一个二进制格式化程序
            BinaryFormatter bf = new BinaryFormatter();
            //打开一个文件流
            FileStream fileStream = File.Open(Application.dataPath + "/Information" + "/SettingInformation.txt", FileMode.Open);
            if (fileStream.Length == 0)
            {
                fileStream.Close();
                return false;
            }
            settingInformation = (SettingInformation)bf.Deserialize(fileStream);
            //关闭文件流
            fileStream.Close();
            return true;
        }
        else
            return false;
    }
}
