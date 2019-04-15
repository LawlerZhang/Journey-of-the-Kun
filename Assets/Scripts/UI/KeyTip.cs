using UnityEngine;
using UnityEngine.UI;
//按键提示
public class KeyTip : MonoBehaviour
{
    [SerializeField] string[] keyTipTexts_Keyboard;
    [SerializeField] string[] keyTipTexts_Joystick;
    [SerializeField] Text value;
    public int keyTipIndex = 0;  //按键提示数组下标
    private void Update()
    {
        if (Input.GetJoystickNames().Length == 0 || Input.GetJoystickNames()[0] == "")
            value.text = keyTipTexts_Keyboard[keyTipIndex];
        else
            value.text = keyTipTexts_Joystick[keyTipIndex];
    }
}
