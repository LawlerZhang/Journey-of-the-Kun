using UnityEngine;

public class AudioInitialize : MonoBehaviour
{
    [SerializeField] GameObject audioManager;
    public void Start()
    {
        foreach (Transform child in audioManager.transform)
        {
            if (child.tag == "GameAudio")
                child.GetComponent<AudioSource>().volume = GameInformation.gameAudioVolume;
            else if (child.tag == "BackgroundAudio")
                child.GetComponent<AudioSource>().volume = GameInformation.backgroundVolume;
        }
    }
}
