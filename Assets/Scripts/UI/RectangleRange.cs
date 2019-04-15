using UnityEngine;
[System.Serializable]
public class RectangleRange
{
    [SerializeField] float topWall;
    [SerializeField] float bottomWall;
    [SerializeField] float leftWall;
    [SerializeField] float rightWall;
    public bool IsMouseInRange()
    {
        float mouseX = Input.mousePosition.x / (float)Screen.width;             //将鼠标x坐标转换为[0,1]的数值
        float mouseY = Input.mousePosition.y / (float)Screen.height;
        if (mouseX >= leftWall && mouseX <= rightWall && mouseY >= bottomWall && mouseY <= topWall)
            return true;
        return false;
    }
}
