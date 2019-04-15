using UnityEngine;

public class ResurgePoint : MonoBehaviour
{
    [SerializeField] int thisIndex;
    GameObject player;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        if (player.transform.position.x > this.transform.position.x)
        {
            player.GetComponent<CharacterInformation>().resurgencePointIndex = thisIndex;
            this.GetComponent<ResurgePoint>().enabled = false;
        }
    }
}
