using UnityEngine;

public class PengController : MonoBehaviour
{
    [SerializeField] float verticalSpeed;
    [SerializeField] float horizontalSpeed;
    [SerializeField] GameObject wind;
    AudioSource blowWindAudioClip;
    float nowTime = 0.0f;
    private void Awake()
    {
        blowWindAudioClip = GameObject.Find("AudioSet/BlowWind").GetComponent<AudioSource>();
    }
    private void Update()
    {
        nowTime += Time.deltaTime;
        if (nowTime >= 10.0f)
        {
            this.transform.GetChild(0).gameObject.SetActive(true);
            this.transform.GetChild(0).gameObject.transform.position = this.transform.position;
            this.transform.GetChild(0).parent = null;
            Destroy(this.gameObject);
        }
        Fly();
        BlowWind();
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
    void BlowWind()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (GameObject.Find("Cyclone(Clone)") == null)
            {
                GameObject.Instantiate(wind, this.transform.position + new Vector3(1.8854f, 0.022f, 0), Quaternion.identity);
                blowWindAudioClip.Play();
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
