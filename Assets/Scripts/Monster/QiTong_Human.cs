using UnityEngine;

public class QiTong_Human : MonoBehaviour
{
    GameObject mainCamera;
    float nowFlyAwayTime = 0.0f;
    private void Awake()
    {
        mainCamera = GameObject.Find("Main Camera");
    }
    private void Start()
    {
        mainCamera.GetComponent<HorizontalSmoothFollow>().cameraMode = "Follow";
    }
    private void Update()
    {
        FlyAway();
    }
    //逃走
    void FlyAway()
    {
        nowFlyAwayTime += Time.deltaTime;
        transform.Translate(4.0f * Time.deltaTime, 2.0f * Time.deltaTime, 0);
        if (nowFlyAwayTime >= 5.0f)
        {
            Destroy(gameObject);
        }
    }
}
