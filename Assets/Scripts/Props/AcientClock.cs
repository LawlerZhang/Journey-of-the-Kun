using UnityEngine;

public class AcientClock : MonoBehaviour
{
    GameObject target;
    [SerializeField] Vector3 startPosition;   //开始飞行的位置
    Vector3 jointPosition;  //一开始在的位置
    [SerializeField] float flySpeed;  //古钟飞行的速度
    [SerializeField] float comeUpSpeed; //古钟往上飞速度
    float posY;
    public int authority = 1;      //权限， 1:MoveToTarget; 2:Falling; 3:ComeUp
    private void Awake()
    {
        jointPosition = this.transform.position;
        posY = startPosition.y;
    }
    private void Start()
    {
        this.GetComponent<Collider2D>().enabled = true;
        this.transform.rotation = Quaternion.Euler(0, 0, 0);
        this.transform.position = startPosition;
    }
    private void Update()
    {
        if (authority == 1) MoveToTarget();
        else if (authority == 2) Falling();
        else if (authority == 3) ComeUp();
    }

    private void MoveToTarget()
    {
        if (target == null || !target.activeInHierarchy)
            target = GameObject.FindWithTag("Player");
        float deltaX = target.transform.position.x - this.transform.position.x;
        if (Mathf.Abs(deltaX) < 0.5f)
        {
            authority = 2;
            Falling();
        }
        if (deltaX > 0)
        {
            this.transform.Translate(flySpeed * Time.deltaTime, 0, 0);
        }
        else if (deltaX < 0)
        {
            this.transform.Translate(-flySpeed * Time.deltaTime, 0, 0);
        }
    }
    void Falling()
    {
        this.GetComponent<Rigidbody2D>().gravityScale = 2.0f;
    }
    void ComeUp()
    {
        this.GetComponent<Rigidbody2D>().gravityScale = 0.0f;
        this.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        if (this.transform.position.y < posY)
        {
            this.transform.Translate(0, comeUpSpeed * Time.deltaTime, 0);
        }
        else
        {
            this.transform.position = new Vector3(this.transform.position.x, posY, this.transform.position.z);
            authority = 1;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Terrain")
        {
            authority = 3;
        }
    }
    //复位
    public void Reset()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
        transform.position = jointPosition;
        GetComponent<Collider2D>().enabled = false;
    }
}
