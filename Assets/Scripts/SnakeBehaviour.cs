using UnityEngine;

public class SnakeBehaviour : MonoBehaviour
{
    GameObject target;
    [SerializeField] Sprite[] move;
    [SerializeField] Sprite dieSprite;
    [SerializeField] float moveSpeed;
    [SerializeField] string snakeColor;
    int direction = 1;
    float nowScale;
    int nowIndex = 0;
    float nowTime = 0;
    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }
    private void Start()
    {
        nowScale = transform.localScale.x;
    }
    private void Update()
    {
        if (target == null || !target.activeInHierarchy)
            target = GameObject.FindGameObjectWithTag("Player");
        if (Mathf.Abs(transform.position.x - target.transform.position.x) >= 20.0f)
        {
            this.GetComponent<MonsterStatus>().isDie = true;
        }
        bool isDie = this.GetComponent<MonsterStatus>().isDie;
        bool isFalled = this.GetComponent<MonsterStatus>().isFalled;
        if (!isDie)
        {
            float deltaX = target.transform.position.x - this.transform.position.x;
            if (Mathf.Abs(deltaX) >= 6.0f)
            {
                if (deltaX > 0)
                    direction = 1;
                else if (deltaX < 0)
                    direction = -1;
                transform.localScale = new Vector3(direction * nowScale, nowScale, 1.0f);
            }
            MoveAnimation(0.3f);
            MoveToTarget();
        }
        if (isDie)
        {
            GetComponent<SpriteRenderer>().sprite = dieSprite;
        }
        if(isFalled)
            Die();
    }
    void MoveToTarget()
    {
        if (target == null || !target.activeInHierarchy)
        {
            target = GameObject.FindGameObjectWithTag("Player");
        }
        transform.Translate(direction * moveSpeed * Time.deltaTime, 0, 0);
    }
    void MoveAnimation(float intervalTime)
    {
        nowTime += Time.deltaTime;
        if (nowTime >= intervalTime)
        {
            nowIndex = (nowIndex + 1) % 3;
            GetComponent<SpriteRenderer>().sprite = move[nowIndex];
            nowTime = 0.0f;
        }
    }
    void Die()
    {
        this.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        this.GetComponent<Rigidbody2D>().gravityScale = 0.0f;
        this.GetComponent<Collider2D>().enabled = false;
        nowTime += Time.deltaTime;
        if (nowTime >= 5.0f)
        {
            /*if (snakeColor == "Green")
                GameInformation.nowGreenSnakesCount--;
            else if (snakeColor == "Pink")
                GameInformation.nowPinkSnakesCount--;*/
            if (this.gameObject.name == "DaWangShe")
                target.GetComponent<CharacterInformation>().energyPoint = 40;
            Destroy(this.gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag=="Water")
        {
            this.GetComponent<MonsterStatus>().isDie = true;
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (this.GetComponent<MonsterStatus>().isDie)
        {
            if (collision.collider.tag == "Water" || collision.collider.tag == "Terrain")
            {
                this.GetComponent<MonsterStatus>().isFalled = true;
            }
        }
    }
}
