using UnityEngine;

public class Wudi : MonoBehaviour {
	void Update ()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            this.GetComponent<CharacterInformation>().healthPoint++; 
        }
        if (Input.GetKey(KeyCode.P))
            this.gameObject.layer = 13;
        else if (Input.GetKey(KeyCode.O))
            this.gameObject.layer = 10;
        else if (Input.GetKey(KeyCode.I))
            this.transform.position = new Vector3(200, 0, -1);
        else if (Input.GetKey(KeyCode.U))
            this.transform.position = new Vector3(345, 0, -1);
    }
}
