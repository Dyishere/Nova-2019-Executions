using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Bullet : MonoBehaviour
{
    private float bulletSpeed = 5.0f;
    private Vector3 direction;
    float damageNum = 0.5f;//每发子弹0.5伤害

    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, 10);///子弹发射没有触碰到物体10秒后消失。
        //returnBullet(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (Pause.GetInstance().GetState() == false)
        {
            transform.Translate(Vector3.Normalize(Vector3.forward) * bulletSpeed * Time.deltaTime);
            //Debug.Log(bulletSpeed);
        }
    }

    void OnCollisionEnter(Collision other)
    {

    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("开始接触");

        if (other.gameObject.tag == "Tower")
        {
            //调用tower扣血函数
        }
        //生成爆炸特效
        Destroy(this.gameObject);
    }
}
