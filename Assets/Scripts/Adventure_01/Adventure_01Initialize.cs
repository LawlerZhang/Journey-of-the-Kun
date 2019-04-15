using UnityEngine;

public class Adventure_01Initialize : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject mainCamera;
    private void Start()
    {
        if (GameInformation.isRevived)
        {
            player.transform.position = new Vector3(GameInformation.nowRevivePostionX, -1.8f, -1.0f);
            mainCamera.transform.position = new Vector3(GameInformation.nowRevivePostionX = 4.0f, 0, -10);
            float temp = GameInformation.nowRevivePostionX;
            if (temp == 55.91f || temp == 103.8f)
            {
                GameInformation.FixedCount = 0;
            }
            else if (temp == 212.25f || temp == 283.6f)
            {
                GameInformation.FixedCount = 1;
            }
            else
                GameInformation.FixedCount = 0;
        }
        else
        {
            GameInformation.FixedCount = 0;
        }
    }
}
