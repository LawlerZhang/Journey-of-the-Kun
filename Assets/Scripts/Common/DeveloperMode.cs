using UnityEngine;

public class DeveloperMode : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKey(KeyCode.F12))
        {
            GameObject player = GameObject.Find("Girl");
            if (player)
            {
                player.GetComponent<CharacterInformation>().healthPoint++;
            }
        }
    }
}
