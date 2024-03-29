﻿using UnityEngine;

public class DiJiangBehaviour : MonoBehaviour
{
    [SerializeField] Color dieColor;
    [SerializeField] GameObject halo;
    GameObject target;
    GameObject canvas;
    float flySpeed = 6.0f;
    float comeUpSpeed = 5.0f;
    float riseUpSpeed = 1.0f;
    bool canFly = true;
    bool canComeUp = false;
    bool isDying = false;   //是否在刚死亡的那一刻
    int direction = 1;
    float size;
    float posY;
    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        canvas = GameObject.Find("Canvas");
        size = this.transform.localScale.x;
        posY = this.transform.position.y;
    }
    private void Update()
    {
        if (target == null || !target.activeInHierarchy)
            target = GameObject.FindGameObjectWithTag("Player");
        if (Vector3.Distance(this.gameObject.transform.position, target.transform.position) >= 20.0f)
        {
            this.GetComponent<MonsterStatus>().isDie = true;
        }
        if (!isDying)
        {
            if (direction == 1)
                this.transform.localScale = new Vector3(size, size, 1);
            else if (direction == -1)
                this.transform.localScale = new Vector3(-size, size, 1);
            MoveToTarget();
            if (canComeUp)
                ComeUp();
        }
        Die();
    }
    private void MoveToTarget()
    {
        float deltaX = target.transform.position.x - this.transform.position.x;
        if (Mathf.Abs(deltaX) < 1.0f)
        {
            Falling();
        }
        if (canFly)
        {
            if (deltaX > 0)
            {
                direction = 1;
                this.transform.Translate(flySpeed * Time.deltaTime, 0, 0);
            }
            else if (deltaX < 0)
            {
                direction = -1;
                this.transform.Translate(-flySpeed * Time.deltaTime, 0, 0);
            }
        }
    }
    void Falling()
    {
        canFly = false;
        this.GetComponent<Rigidbody2D>().gravityScale = 2.0f;
    }
    void ComeUp()
    {
        if (this.transform.position.y < posY)
        {
            this.transform.Translate(direction * comeUpSpeed * Mathf.Sin(0.785f) * Time.deltaTime, comeUpSpeed * Mathf.Cos(0.785f) * Time.deltaTime, 0);
        }
        else
        {
            this.transform.position = new Vector3(this.transform.position.x, posY, this.transform.position.z);
            canComeUp = false;
            canFly = true;
        }
    }
    void RiseUp()
    {
        this.transform.Translate(0, riseUpSpeed * Time.deltaTime, 0);
        if (this.transform.position.y >= posY + 3.0f)
        {
            if(this.gameObject.name=="DaDiJiang")
            {
                canvas.transform.GetChild(9).gameObject.SetActive(true);
            }
            Destroy(this.gameObject);
        }
    }
    void Die()
    {
        if (this.GetComponent<MonsterStatus>().isDie)
        {
            if (!isDying)
            {
                this.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                this.GetComponent<Rigidbody2D>().gravityScale = 0.0f;
                this.GetComponent<Collider2D>().enabled = false;
                canFly = false;
                canComeUp = false;
                this.GetComponent<SpriteRenderer>().color = dieColor;
                halo.SetActive(true);
                isDying = true;
            }
            RiseUp();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Terrain" || collision.collider.tag == "Water")
        {
            this.GetComponent<Rigidbody2D>().gravityScale = 0.0f;
            canComeUp = true;
        }
    }
}
