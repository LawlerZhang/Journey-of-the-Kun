using UnityEngine;

[System.Serializable]
public class SettingInformation
{
    public float backgroundVolume = 1.0f;
    public float gameAudioVolume = 1.0f;
    public int languageIndex = 0;
    public int screenSettingIndex = 0;   //只能取0或1
    public int resolutionIndex = 7;      //屏幕分辨率数组下标，分辨率分别是
}
