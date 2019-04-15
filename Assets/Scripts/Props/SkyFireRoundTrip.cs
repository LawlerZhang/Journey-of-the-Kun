using UnityEngine;

public class SkyFireRoundTrip : MonoBehaviour
{
    [SerializeField] GameObject skyFire;
    float startX;
    [SerializeField] float terminalX;
    int direction = 1;
    float nowMoveTime = 0.0f;
    private void Awake()
    {
        startX = transform.position.x;
    }
    private void Update()
    {
        RoundTrip();
        DropFire();
    }
    void RoundTrip()
    {
        transform.Translate(direction * Time.deltaTime, 0, 0);
        if (transform.position.x > terminalX)
            direction = -1;
        else if (transform.position.x < startX)
            direction = 1;
    }
    void DropFire()
    {
        nowMoveTime += Time.deltaTime;
        if (nowMoveTime >= 5.0f)
        {
            GameObject.Instantiate(skyFire, transform.position, Quaternion.identity);
            nowMoveTime = 0.0f;
        }
    }
}
