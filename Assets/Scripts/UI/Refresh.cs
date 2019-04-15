using UnityEngine;
using UnityEngine.SceneManagement;

public class Refresh : MonoBehaviour
{
    public void Click()
    {
        GameInformation.isRevived = false;
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Adventure_01");
    }
}
