using UnityEngine;

public class GameScreen : MonoBehaviour
{
    public static bool IsInScreen(GameObject target, GameObject mainCamera)
    {
        float width_height_percent = (float)Screen.width / (float)Screen.height;
        float deltaX = mainCamera.GetComponent<Camera>().orthographicSize * width_height_percent;
        if (target.transform.position.x >= mainCamera.transform.position.x + deltaX || target.transform.position.x <= mainCamera.transform.position.x - deltaX)
            return false;
        else
            return true;
    }
}
