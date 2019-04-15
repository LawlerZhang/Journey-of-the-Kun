using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnMainMenu : MonoBehaviour
{
    public void Click()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("MenuScene");
    }
}
