using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectButton : MonoBehaviour
{
    [SerializeField] GameObject[] buttons;
    [SerializeField] GameObject fadeMask;
    [SerializeField] GameObject menu;
    [SerializeField] GameObject systemSetting;
    [SerializeField] GameObject producers;
    [SerializeField] GameObject menuAdventure_01;
    [SerializeField] GameObject operateGuide;
    [SerializeField] AudioSource switchClip;
    [SerializeField] RectangleRange[] rectRanges;
    [SerializeField] Color commonColor;
    [SerializeField] Color selectedColor;
    [SerializeField] string sceneName;  //当前场景的名字
    bool canSubmitByMouse = false;
    bool isOvered = false;  //表示鼠标是否已经在按钮上过
    public static bool isFaded = false;
    int nowIndex = 0;
    float preRealTime = 0.0f;
    float nowRealTime = 0.0f;
    public int authority = 0; //权限控制
    void Update ()
    {
        if(authority==0||authority==1)
            SelectByKeyOrJoystick();
        if (authority == 0 || authority == 2)
            SelectByMouse();
        if (authority == 0 || authority == 3)
            Submit();
        Exit();
    }
    void SelectByKeyOrJoystick()
    {
        float deltaY = Input.GetAxis("Vertical");
        nowRealTime = Time.realtimeSinceStartup;
        //由于Time.timeScale==0时候GetAxis无法获得键盘输入，故单独处理
        if (Time.timeScale == 0 && deltaY == 0.0f)
        {
            if (Input.GetKey(KeyCode.UpArrow))
                deltaY = 1.0f;
            else if (Input.GetKey(KeyCode.DownArrow))
                deltaY = -1.0f;
            else
                deltaY = 0.0f;
        }
        if (deltaY < 0.0f && nowRealTime - preRealTime >= 0.25f)
        {
            switchClip.Play();
            nowIndex = (nowIndex + 1) % buttons.Length;
            ColorChange();
            isOvered = false;
            deltaY = 0.0f;
            preRealTime = nowRealTime;
        }
        else if (deltaY > 0.0f && nowRealTime - preRealTime >= 0.25f)
        {
            switchClip.Play();
            if (nowIndex != 0) nowIndex--;
            else nowIndex = buttons.Length - 1;
            ColorChange();
            isOvered = false;
            deltaY = 0.0f;
            preRealTime = nowRealTime;
        }
    }
    void SelectByMouse()
    {
        bool isInRange = false;
        for (int i = 0; i < rectRanges.Length; i++)
        {
            if (rectRanges[i].IsMouseInRange())
            {
                if (!isOvered)
                {
                    switchClip.Play();
                    isOvered = true;
                    nowIndex = i;
                    ColorChange();
                    canSubmitByMouse = true;
                }
                isInRange = true;
                break;
            }
        }
        if (!isInRange)
        {
            canSubmitByMouse = false;
            isOvered = false;
        }
    }
    void Submit()
    {
        if (Input.GetButtonDown("Submit") || (canSubmitByMouse && Input.GetMouseButtonDown(0) || isFaded))
        {
            if (!isFaded && fadeMask != null)
            {
                authority = 3;
                fadeMask.SetActive(true);
                return;
            }
            switch (buttons[nowIndex].name)
            {
                case "NewGame": SceneManager.LoadScene("Adventure_01"); break;
                case "ContinueGame": SceneManager.LoadScene("Adventure_01"); break;
                case "Setting": SceneManager.LoadScene("Setting");break;
                case "ExitGame": Application.Quit(); break;
                case "OperateIntroduction":
                    {
                        if(operateGuide!=null)
                        {              
                            operateGuide.SetActive(true);
                            gameObject.SetActive(false);
                        }
                        break;
                    }
                case "SystemSetting":
                    {
                        if (systemSetting != null)
                            systemSetting.SetActive(true);
                        if (menu != null)
                            menu.SetActive(false);
                        gameObject.SetActive(false);
                        break;
                    }
                case "Producers":
                    {
                        gameObject.SetActive(false);
                        menu.SetActive(false);
                        producers.SetActive(true);
                        break;
                    }
                case "ReturnMainMenu":
                    {
                        Time.timeScale = 1.0f;
                        SceneManager.LoadScene("MenuScene");
                        break;
                    }
                case "RestartAdventure_01":
                    {
                        Time.timeScale = 1.0f;
                        SceneManager.LoadScene("Adventure_01");
                        break;
                    }
                case "ContinueAdventure_01":
                    {
                        Time.timeScale = 1.0f;
                        this.gameObject.SetActive(false);
                        menuAdventure_01.SetActive(false);
                        break;
                    }
                default: break;
            }
            isFaded = false;
            authority = 0;
        }
    }
    void ColorChange()
    {
        buttons[nowIndex].GetComponent<Text>().color = selectedColor;
        for (int i = nowIndex + 1; i % buttons.Length != nowIndex; i++)
        {
            buttons[i % buttons.Length].GetComponent<Text>().color = commonColor;
        }
    }
    void Exit()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (sceneName == "Setting")
                SceneManager.LoadScene("MenuScene");
        }
    }
}
