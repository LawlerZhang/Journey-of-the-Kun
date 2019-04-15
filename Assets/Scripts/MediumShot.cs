using UnityEngine;
//此脚本用于控制中景移动
public class MediumShot : MonoBehaviour
{
    [SerializeField] GameObject mainCamera;
    [SerializeField] float cameraStartX;
    [SerializeField] float moveRate;   //中景移动相对于远景比例
    private void LateUpdate()
    {
        float deltaX = mainCamera.transform.position.x - cameraStartX;
        this.transform.position = new Vector3(moveRate * deltaX, 0, 0);
    }
}
