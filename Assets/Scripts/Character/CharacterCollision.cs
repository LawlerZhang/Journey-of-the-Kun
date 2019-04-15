using UnityEngine;

public class CharacterCollision : MonoBehaviour
{
    [SerializeField] GameObject refreshButton;
    [SerializeField] Color[] nowColor;
    [SerializeField] AudioSource hurtAudioClip;
    [SerializeField] AudioSource drowningAudioClip;
    [SerializeField] GameObject[] ResurgencePoints;
    Animator animator;
    int nowIndex = 0;
    bool canTwinkle = false;
    int nowTwinkledCount = 0;
    float nowTime = 0.0f;
    private void Awake()
    {
        animator = this.GetComponent<Animator>();
    }
    private void Update()
    {
        if (canTwinkle)
        {
            TwinkleSelf(0.2f);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.collider.tag)
        {
            case "Terrain":
                {
                    GetComponent<CharacterAction>().jumpCount = 0;
                    GetComponent<CharacterAction>().isInAir = false;
                    break;
                }
            case "Platform":
                {
                    if (this.transform.position.y - collision.collider.transform.position.y >= 1.0f)
                    {
                        GetComponent<CharacterAction>().jumpCount = 0;
                        GetComponent<CharacterAction>().isInAir = false;
                    }
                    else
                        GetComponent<CharacterAction>().jumpCount = 2;
                    break;
                }
            case "Monster":
                {
                    hurtAudioClip.Play();
                    animator.SetTrigger("Hurt");
                    if (GetComponent<CharacterInformation>().healthPoint > 0)
                    {
                        GetComponent<CharacterInformation>().healthPoint--;
                        gameObject.layer = 8;
                        if (this.GetComponent<CharacterInformation>().healthPoint != 0)
                            canTwinkle = true;
                    }
                    break;
                }
            case "Water":
                {
                    drowningAudioClip.Play();
                    if (GetComponent<CharacterInformation>().healthPoint > 0)
                    {
                        GetComponent<CharacterInformation>().healthPoint--;
                        if (GetComponent<CharacterInformation>().healthPoint > 0 && GetComponent<CharacterInformation>().resurgencePointIndex != -1)
                            this.transform.position = ResurgencePoints[GetComponent<CharacterInformation>().resurgencePointIndex].transform.position;
                    }
                    break;
                }
            case "EnemyWeapon":
                {
                    hurtAudioClip.Play();
                    if (GetComponent<CharacterInformation>().healthPoint > 0)
                    {
                        GetComponent<CharacterInformation>().healthPoint--;
                        gameObject.layer = 8;
                        canTwinkle = true;
                    }
                    break;
                }
            case "AncientClock":
                {
                    hurtAudioClip.Play();
                    animator.SetTrigger("Hurt");
                    if (GetComponent<CharacterInformation>().healthPoint > 0)
                    {
                        GetComponent<CharacterInformation>().healthPoint--;
                        gameObject.layer = 8;
                        if (this.GetComponent<CharacterInformation>().healthPoint != 0)
                            canTwinkle = true;
                    }
                    break;
                }
        }
    }
    //被攻击后自身的闪烁效果
    void TwinkleSelf(float intervalTime)
    {
        nowTime += Time.deltaTime;
        if (nowTime >= intervalTime)
        {
            nowTwinkledCount++;
            nowIndex ^= 1;
            GetComponent<SpriteRenderer>().color = nowColor[nowIndex];
            nowTime = 0.0f;
        }
        if (nowTwinkledCount >= 10)
        {
            gameObject.layer = 10;
            nowTime = 0.0f;
            nowIndex = 0;
            nowTwinkledCount = 0;
            canTwinkle = false;
        }
    }
}
