using UnityEngine;

public class MuscialInstruction : MonoBehaviour
{
    float nowTime = 0.0f;
    [SerializeField] float flySpeed;
    private void Update()
    {
        nowTime += Time.deltaTime;
        this.transform.Translate(-flySpeed * Time.deltaTime, 0, 0);
        if (nowTime >= 3.0f)
            Destroy(this.gameObject);
    }
}
