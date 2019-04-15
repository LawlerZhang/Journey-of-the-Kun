using UnityEngine;

public class TurtleShell : MonoBehaviour
{
    float startPositionX;
    [SerializeField] float terminalPositionX;
    int direction = 1;
    private void Awake()
    {
        startPositionX = transform.position.x;
    }
    private void Update()
    {
        RoundTrip();
    }
    void RoundTrip()
    {
        transform.Translate(direction* 2.5f * Time.deltaTime, 0, 0);
        if (transform.position.x >= terminalPositionX)
            direction = -1;
        else if (transform.position.x <= startPositionX)
            direction = 1;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            collision.collider.transform.parent = this.transform;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            collision.collider.transform.parent = null;
        }
    }
}
