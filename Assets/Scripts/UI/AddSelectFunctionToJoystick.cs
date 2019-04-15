using System;
using System.Runtime.InteropServices;
using UnityEngine;
//此脚本给手柄添加可以点击按钮的功能
public class AddSelectFunctionToJoystick : MonoBehaviour
{
    public struct RECT
    {
        public int left;
        public int top;
        public int right;
        public int bottom;
        public RECT(int left, int top, int right, int bottom)
        {
            this.left = left;
            this.top = top;
            this.right = right;
            this.bottom = bottom;
        }
    }
    [DllImport("user32.dll")]
    public static extern int SetCursorPos(int x, int y);
    [DllImport("user32.dll")] //获得窗口的句柄
    public static extern System.IntPtr FindWindowEx(System.IntPtr parent, System.IntPtr childe, string strclass, string strname);
    [DllImport("user32.dll", CharSet = CharSet.Auto)]//根据句柄获得位置
    public static extern bool GetWindowRect(IntPtr hWnd, ref RECT pos);
    [DllImport("user32.dll")]
    static extern bool ClipCursor(ref RECT rect);
    int cursorPosX_Window;
    int cursorPosY_Window;
    int cursorPosX_Screen;
    int cursorPosY_Screen;
    int amendX;     //由于根据句柄获得的窗口位置有偏差，所以用amendX和amendY进行修正
    int amendY;
    int moveExtent;   //每秒钟移动的像素点数目
    bool isJoysticking = false;  //当前手柄是否在输入
    RECT position;
    Resolution maxResolution;    //设备的最大分辨率
    private void Awake()
    {
        SetCursorPos(0, 0);
        IntPtr p = FindWindowEx(System.IntPtr.Zero, System.IntPtr.Zero, null, "Test2D");//"Test2D"是游戏的窗体名
        position = new RECT();
        GetWindowRect(p, ref position);
        maxResolution = Screen.resolutions[Screen.resolutions.Length - 1];
    }
    private void Start()
    {
        amendX = position.left - (int)Input.mousePosition.x;
        amendY = (int)Input.mousePosition.y - Screen.height;
    }
    private void Update()
    {
        MoveCursor();
        Submit();
    }
    void MoveCursor()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        if (horizontal != 0.0f || vertical != 0.0f)
            isJoysticking = true;
        if (isJoysticking)
        {
            moveExtent = Screen.width / 2;
            IntPtr p = FindWindowEx(System.IntPtr.Zero, System.IntPtr.Zero, null, "Test2D");//"Test2D"是游戏的窗体名
            RECT position = new RECT();
            GetWindowRect(p, ref position);
            RECT range = new RECT(position.left + amendX, position.top + amendY, position.right + amendX, position.bottom + amendY);
            ClipCursor(ref range);
            cursorPosX_Window = (int)Input.mousePosition.x;
            cursorPosY_Window = (int)Input.mousePosition.y;
            cursorPosX_Screen = position.left + cursorPosX_Window + amendX;
            cursorPosY_Screen = position.top + Screen.height - cursorPosY_Window + amendY;
            isJoysticking = true;
            cursorPosX_Screen += (int)(moveExtent * Time.deltaTime * horizontal);
            cursorPosY_Screen -= (int)(moveExtent * Time.deltaTime * vertical);
            SetCursorPos(cursorPosX_Screen, cursorPosY_Screen);
            cursorPosX_Screen = position.left + cursorPosX_Window;
        }
        else
        {
            RECT range = new RECT(0, 0, maxResolution.width, maxResolution.height);
            ClipCursor(ref range);
            isJoysticking = false;
        }
    }
    void Submit()
    {
        if(Input.GetButtonDown("Submit"))
        {
            Input.GetMouseButtonDown(0);
        }
    }
}
