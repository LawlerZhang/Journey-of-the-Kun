using UnityEngine;
using UnityEngine.UI;

public class DartBehaviour : MonoBehaviour
{
    [SerializeField] float rotateSpeed = 180.0f;
    [SerializeField] float flySpeed = 20.0f;
    int ATK;
    [SerializeField] float minAngle;  //飞镖飞行的最小角度
    [SerializeField] float maxAngle;  //飞镖飞行的最大角度
    float flyAngle;    //飞镖飞行真实的角度
    AudioSource bloodClip;
    AudioSource stabIntoTerrainAudioClip;
    AudioSource drowningAudioClip;
    GameObject owner;
    GameObject mainCamera;
    public float actionBarSize;   //动作条的大小
    bool isSticked = false;
    bool isShotted = false;   //是否已经射出
    float nowTime = 0.0f;
    int horizontalDirection = 1;     //水平方向的方向，1代表右，-1代表左
    private void Awake()
    {
        ATK = GameInformation.ATK;
        owner = GameObject.Find("Girl");
        if (owner == null)
            Destroy(this.gameObject);
        if (owner != null)
        {
            mainCamera = GameObject.Find("Main Camera");
            bloodClip = GameObject.Find("AudioSet/SnakeBlood").GetComponent<AudioSource>();
            stabIntoTerrainAudioClip = GameObject.Find("AudioSet/StabIntoTerrain").GetComponent<AudioSource>();
            drowningAudioClip = GameObject.Find("AudioSet/Drowning").GetComponent<AudioSource>();
        }
    }
    private void Start()
    {
        horizontalDirection = owner.GetComponent<CharacterAction>().direction;
    }
    private void Update()
    {
        if (!isShotted)
        {
            Fly();
            isShotted = true;
        }
        if (!isSticked)
        {
            RotateSelf();
        }
        DestroyOffScreen();
        if (isSticked)
            DestroySelf();
    }
    void RotateSelf()
    {
        this.transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);
    }
    void Fly()
    {
        flyAngle = (maxAngle - minAngle) * actionBarSize + minAngle;
        flyAngle = flyAngle * 100 * (Mathf.PI / 180.0f);
        flySpeed += 15.0f * actionBarSize;
        GetComponent<Rigidbody2D>().AddForce(new Vector2(horizontalDirection * Mathf.Cos(flyAngle) * flySpeed, Mathf.Sin(flyAngle) * flySpeed));
    }
    void DestroyOffScreen()
    {
        float width_height_percent = (float)Screen.width / (float)Screen.height;
        float deltaX = mainCamera.GetComponent<Camera>().orthographicSize * width_height_percent;
        if (transform.position.x >= mainCamera.transform.position.x + deltaX || transform.position.x <= mainCamera.transform.position.x - deltaX)
            Destroy(this.gameObject);
    }
    private void DestroySelf()
    {
        nowTime += Time.deltaTime;
        if (nowTime >= 4.0f)
            Destroy(this.gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Monster")
        {
            bloodClip.Play();
            if (owner.GetComponent<CharacterInformation>().energyPoint < 40)
                owner.GetComponent<CharacterInformation>().energyPoint++;
            collision.gameObject.GetComponent<MonsterStatus>().healthPoint -= ATK;
            Destroy(this.gameObject);
        }
        if (collision.collider.tag == "Terrain" || collision.collider.tag == "Plant" || collision.collider.tag == "Platform")
        {
            isSticked = true;
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            this.GetComponent<Rigidbody2D>().gravityScale = 0.0F;
            this.GetComponent<Collider2D>().enabled = false;
            stabIntoTerrainAudioClip.Play();
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, collision.collider.transform.position.z + 0.1f);
            this.transform.parent = collision.transform;
        }
        if (collision.collider.tag == "Water")
        {
            drowningAudioClip.Play();
            Destroy(this.gameObject);
        }
        if (collision.collider.name == "QiTong_BeastState")
        {
            collision.collider.gameObject.GetComponent<QiTongBehaviour>().isAttacked = true;
        }
        if (collision.collider.tag == "EnemyWeapon")
        {
            Destroy(this.gameObject);
        }
        if(collision.collider.tag=="AncientClock")
        {
            GameObject.Find("AudioSet/AncientClockRing").GetComponent<AudioSource>().Play();
            Destroy(this.gameObject);
        }
    }
}
