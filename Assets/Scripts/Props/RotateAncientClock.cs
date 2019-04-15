using UnityEngine;

public class RotateAncientClock : MonoBehaviour
{
    [SerializeField] float minAngle;
    [SerializeField] float maxAngle;
    [SerializeField] float rotateSpeed;
    float nowRotation = 0.0f;       //当前的旋转角度
    [SerializeField] int rotateDirection;     //旋转的方向，1代表向右旋转，-1代表向左旋转
    private void Update()
    {
        Swing();
    }
    private void OnDisable()
    {
        this.transform.rotation = Quaternion.identity;
        nowRotation = 0.0f;
    }
    /*private void OnEnable()
    {
        
    }*/
    void Swing()
    {
        float rotatePerFrame = rotateDirection * rotateSpeed * Time.deltaTime;
        this.transform.Rotate(0, 0, rotatePerFrame);
        nowRotation += rotatePerFrame;
        if (nowRotation >= maxAngle)
        {
            nowRotation = maxAngle;
            this.transform.rotation = Quaternion.Euler(0, 0, maxAngle);
            rotateDirection = -1;
        }
        else if (nowRotation <= minAngle)
        {
            nowRotation = minAngle;
            this.transform.rotation = Quaternion.Euler(0, 0, minAngle);
            rotateDirection = 1;
        }
    }
}
