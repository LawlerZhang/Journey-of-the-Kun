using UnityEngine;

public class CharacterAction : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float upSpeed;
    [SerializeField] Vector3 bossPoint;
    GameObject mainCamera;
    AudioSource DashAudioClip;
    float size;
    public int direction = 1;   //前进的方向，1表示向右，-1表示向左
    public int jumpCount = 0; //当前跳跃了几下，最多允许两段跳
    float preDeltaX = 0.0f;
    int authority = 0;   //动作权限，-1不可进行任何动作，0可以进行任何操作，1跑，2跳，3冲刺
    float nowTime = 0.0f;
    float dashCD = 0.0f;      //冲撞现在的CD时间
    bool canCountCD = true;  //是否可以开始计算CD
    public bool isInAir = false;    //人物是否在空中
    private void Awake()
    {
        mainCamera = GameObject.Find("Main Camera");
        animator = GetComponent<Animator>();
        DashAudioClip = GameObject.Find("AudioSet/Dash").GetComponent<AudioSource>();
        size = transform.localScale.x;
    }
    private void Update()
    {
        Boundary();
        if (Time.timeScale != 0.0f)
        {
            size = Mathf.Abs(transform.localScale.x);
            if (Vector3.Distance(bossPoint, this.transform.position) < 0.3f)
            {
                this.transform.position = new Vector3(344.0f, 0.0f, -1.0f);
            }
            if (authority == 0 || authority == 1) Run();
            if (authority == 0 || authority == 2) Jump();
            if (authority == 0 || authority == 3) Dash();
        }
        //如果相机是“固定”模式，则要将角色限制在屏幕范围内
        if (mainCamera.GetComponent<HorizontalSmoothFollow>().cameraMode == "Fixed")
        {
            AstrictGameObjectInScreen(this.gameObject, mainCamera);
        }
    }
    void Run()
    {
        float deltaX = Input.GetAxis("Horizontal");
        if (deltaX != 0.0f && preDeltaX == 0.0f && !isInAir)
        {
            animator.SetTrigger("Run");
        }
        if (deltaX != 0.0f)
        {
            direction = deltaX > 0.0f ? 1 : -1;
            transform.localScale = new Vector3(direction * size, size, 1.0f);
            this.transform.Translate(direction * moveSpeed * Time.deltaTime, 0, 0);
        }
        if (deltaX == 0.0f && preDeltaX == 0.0f && !isInAir)
        {
            //animator.ResetTrigger("Run");
            animator.SetTrigger("Idle");
        }
        if (deltaX != 0.0f && preDeltaX != 0.0f && !isInAir)
        {
            //animator.ResetTrigger("Idle");
            animator.SetTrigger("Run");
        }
        preDeltaX = deltaX;
    }
    void Jump()
    {
        if (jumpCount < 2)
        {
            if (Input.GetButtonDown("Jump"))
            {
                isInAir = true;
                //animator.ResetTrigger("Idle");
                animator.SetTrigger("Jump");
                jumpCount++;
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, upSpeed);
                this.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, upSpeed));
                if (jumpCount == 2)
                {
                    isInAir = true;
                    animator.SetTrigger("Jump");
                    this.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, upSpeed));
                }
            }
        }
        /*if (Input.GetButtonUp("Jump"))
        {
            animator.SetTrigger("Idle");
        }*/
    }
    //冲刺动作
    void Dash()
    {
        if (canCountCD)
        {
            dashCD += Time.deltaTime;
        }
        if (Input.GetButtonDown("Dash") && dashCD >= 1.0f)
        {
            if (authority != 3)
            {
                canCountCD = false;
                this.GetComponent<SpriteRenderer>().enabled = false;
                DashAudioClip.Play();
                authority = 3;
                this.gameObject.layer = 8;
            }
        }
        if (authority == 3)
        {
            nowTime += Time.deltaTime;
            this.transform.Translate(direction * 12.0f * Time.deltaTime, 0, 0);
            if (nowTime >= 0.4f)
            {
                canCountCD = true;
                dashCD = 0.0f;
                this.GetComponent<SpriteRenderer>().enabled = true;
                nowTime = 0.0f;
                authority = 0;
                this.gameObject.layer = 10;
            }
        }
    }
    //限制游戏对象在屏幕范围内
    void AstrictGameObjectInScreen(GameObject gameObject, GameObject mainCamera)
    {
        float width_height_percent = (float)Screen.width / (float)Screen.height;    //宽高比
        float deltaX = mainCamera.GetComponent<Camera>().orthographicSize * width_height_percent;  //当前屏幕一半距离在游戏中对应的米数
        if (gameObject.transform.position.x >= mainCamera.transform.position.x + deltaX - 0.5f)
        {
            gameObject.transform.position = new Vector3(mainCamera.transform.position.x + deltaX - 0.5f, gameObject.transform.position.y, gameObject.transform.position.z);
        }
        else if (gameObject.transform.position.x <= mainCamera.transform.position.x - deltaX + 0.5f)
        {
            gameObject.transform.position = new Vector3(mainCamera.transform.position.x - deltaX + 0.5f, gameObject.transform.position.y, gameObject.transform.position.z);
        }
    }
    void Boundary()
    {
        if (transform.position.x <= -10.0f)
            transform.position = new Vector3(-10.0f, transform.position.y, transform.position.z);
        if (transform.position.x >= 384.0f)
            transform.position = new Vector3(384.0f, transform.position.y, transform.position.z);
    }
}
