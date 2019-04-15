using UnityEngine;

public class GameInformation
{
    public static float backgroundVolume = 1.0f;
    public static float gameAudioVolume = 1.0f;
    public static int languageIndex = 0;
    public static int screenSettingIndex = 0;   //只能取0或1
    public static int resolutionIndex = 7;      //屏幕分辨率数组下标，分辨率分别是
    public static float nowRevivePostionX = 0.0f;
    public static int FixedCount = 0;
    public static bool isRevived = false;
    public static int ATK = 2;

    //public static int nowDijiangCount = 0;
    //public static int nowGreenSnakesCount = 0;
    //public static int nowPinkSnakesCount = 0;
}
