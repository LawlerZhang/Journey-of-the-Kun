using UnityEngine;

public class HorizontalSmoothFollow : MonoBehaviour
{
    [SerializeField] GameObject target;
    [SerializeField] float place1;     //当人物到达该位置开始固定视角
    [SerializeField] float place2;     //当人物到达该位置开始固定视角
    public string cameraMode = "Follow"; //当前的相机模式，有Follow和Fixed两种
    float smoothing = 3.0f;
    float deltaX;
    private void Awake()
    {
        deltaX = target.transform.position.x - this.transform.position.x;
    }
    private void LateUpdate()
    {
        if (cameraMode == "Follow")
            this.transform.position = Vector3.Lerp(this.transform.position, new Vector3(target.transform.position.x - deltaX, this.transform.position.y, this.transform.position.z), smoothing * Time.deltaTime);
        else if (cameraMode == "Fixed" && GameInformation.FixedCount == 1)
            this.transform.position = Vector3.Lerp(this.transform.position, new Vector3(145.0f, this.transform.position.y, this.transform.position.z), smoothing * Time.deltaTime);
        else if (cameraMode == "Fixed" && GameInformation.FixedCount == 2)
            this.transform.position = Vector3.Lerp(this.transform.position, new Vector3(360.0f, this.transform.position.y, this.transform.position.z), smoothing * Time.deltaTime);
        if ((target.transform.position.x >= place1 && cameraMode != "Fixed" && GameInformation.FixedCount == 0))
        {
            cameraMode = "Fixed";   //变成固定视角
            GameInformation.FixedCount++;
        }
        else if ((target.transform.position.x >= place2 && cameraMode != "Fixed" && GameInformation.FixedCount == 1))
        {
            cameraMode = "Fixed";   //变成固定视角
            GameInformation.FixedCount++;
        }
    }
}
