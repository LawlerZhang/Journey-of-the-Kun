using UnityEngine;

public class KunController : MonoBehaviour
{
    [SerializeField] float verticalSpeed;
    [SerializeField] float horizontalSpeed;
    [SerializeField] GameObject fire;
    AudioSource sprayFireAuidoClip;
    float nowTime = 0.0f;
    private void Awake()
    {
        sprayFireAuidoClip = GameObject.Find("AudioSet/SprayFire").GetComponent<AudioSource>();
    }
    private void Update()
    {
        nowTime += Time.deltaTime;
        if (nowTime >= 3.0f)
        {
            this.transform.GetChild(0).gameObject.SetActive(true);
            this.transform.GetChild(0).gameObject.transform.position = this.transform.position;
            this.transform.GetChild(0).parent = null;
            Destroy(this.gameObject);
        }
        Fly();
        Fire();
    }
    void Fly()
    {
        float deltaY = Input.GetAxis("Vertical");
        if (deltaY > 0)
            this.transform.Translate(0, verticalSpeed * Time.deltaTime, 0);
        if (deltaY < 0)
            this.transform.Translate(0, -verticalSpeed * Time.deltaTime, 0);
        float deltaX = Input.GetAxis("Horizontal");
        if (deltaX > 0)
            this.transform.Translate(horizontalSpeed * Time.deltaTime, 0, 0);
        if (deltaX < 0)
            this.transform.Translate(-horizontalSpeed * Time.deltaTime, 0, 0);
    }
    void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (this.transform.childCount == 0)
            {
                GameObject newFire;
                newFire = GameObject.Instantiate(fire, this.transform.position + new Vector3(1.8854f, 0.022f, 0), Quaternion.identity);
                newFire.transform.parent = this.gameObject.transform;
                sprayFireAuidoClip.Play();
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Monster" || collision.collider.tag == "EnemyWeapon")
        {
            this.transform.GetChild(0).gameObject.SetActive(true);
            this.transform.GetChild(0).gameObject.transform.position = this.transform.position;
            this.transform.GetChild(0).parent = null;
            Destroy(this.gameObject);
        }
    }
}
