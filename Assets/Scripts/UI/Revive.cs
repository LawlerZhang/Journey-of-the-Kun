using UnityEngine;
using UnityEngine.SceneManagement;

public class Revive : MonoBehaviour
{
    public void Click()
    {
        GameInformation.isRevived = true;
        Time.timeScale = 1;
        SceneManager.LoadScene("Adventure_01");
    }
}
