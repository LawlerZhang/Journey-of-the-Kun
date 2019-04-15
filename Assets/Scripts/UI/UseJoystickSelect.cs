using UnityEngine;
using UnityEngine.UI;

public class UseJoystickSelect : MonoBehaviour
{
    [SerializeField] GameObject[] buttons;
    [SerializeField] int rowCount;
    [SerializeField] int columnCount;
    int length;
    int nowIndex = 0;
    float preRealTime = 0.0f;
    float nowRealTime = 0.0f;
    private void Awake()
    {
        length = rowCount * columnCount;
    }
    private void Update()
    {
        SelectButton();
        ChangeColor();
        Submit();
    }
    void SelectButton()
    {
        nowRealTime = Time.realtimeSinceStartup;
        if (nowRealTime - preRealTime >= 0.15f)
        {
            preRealTime = nowRealTime;
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            if (Time.timeScale == 0.0f && horizontal == 0.0f && vertical == 0.0f)
            {
                if (Input.GetKey(KeyCode.UpArrow))
                    vertical = 1.0f;
                else if (Input.GetKey(KeyCode.DownArrow))
                    vertical = -1.0f;
                else if (Input.GetKey(KeyCode.LeftArrow))
                    horizontal = -1.0f;
                else if (Input.GetKey(KeyCode.RightArrow))
                    horizontal = 1.0f;
                else
                {
                    horizontal = 0.0f;
                    vertical = 0.0f;
                }
            }
            if (Mathf.Abs(horizontal) > Mathf.Abs(vertical))
            {
                if (horizontal > 0.0f)
                    nowIndex = (nowIndex + 1) % length;
                else if (horizontal < 0.0f)
                    nowIndex = nowIndex - 1 < 0 ? length - 1 : nowIndex - 1;
            }
            else if (Mathf.Abs(horizontal) < Mathf.Abs(vertical))
            {
                if (vertical < 0.0f)
                    nowIndex = (nowIndex + columnCount) % length;
                else if (vertical > 0.0f)
                    nowIndex = nowIndex - columnCount < 0 ? length - columnCount + nowIndex : nowIndex - columnCount;
            }
        }
    }
    void ChangeColor()
    {
        buttons[nowIndex].GetComponent<Image>().color = buttons[nowIndex].GetComponent<ScriptName>().selectedColor;
        for (int i = (nowIndex + 1) % length; i != nowIndex; i = (i + 1) % length)
        {
            buttons[i].GetComponent<Image>().color = buttons[i].GetComponent<ScriptName>().commonColor;
        }
    }
    void Submit()
    {
        if (Input.GetButtonDown("Submit"))
        {
            string direction = buttons[nowIndex].GetComponent<ScriptName>().direction;
            switch (buttons[nowIndex].GetComponent<ScriptName>().scriptName)
            {
                case "TurnUpDown":
                    {
                        if (direction == "Up")
                            buttons[nowIndex].GetComponent<TurnUpDown>().TurnUp();
                        else if (direction == "Down")
                            buttons[nowIndex].GetComponent<TurnUpDown>().TurnDown();
                        break;
                    }
                case "ReturnGame":
                    {
                        buttons[nowIndex].GetComponent<ReturnGame>().Click();
                        break;
                    }
                case "Refresh":
                    {
                        buttons[nowIndex].GetComponent<Refresh>().Click();
                        break;
                    }
                case "SaveSettingInformation":
                    {
                        buttons[nowIndex].GetComponent<SaveSettingInformation>().Click();
                        break;
                    }
                case "ProducerToMenu":
                    {
                        buttons[nowIndex].GetComponent<ProducerToMenu>().Click();
                        break;
                    }
                case "ReturnMainMenu":
                    {
                        buttons[nowIndex].GetComponent<ReturnMainMenu>().Click();
                        break;
                    }
                case "Revive":
                    {
                        buttons[nowIndex].GetComponent<Revive>().Click();
                        break;
                    }
                default: break;
            }
        }
    }
}
