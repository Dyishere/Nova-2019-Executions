using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyCreate : MonoBehaviour
{
    //public GameObject[] EnemyArray;
    public GameObject EnemyPre;
    private Transform pos;
    private int EnemyCurrentNum;//当前敌人数目
    public int EnemyMaxNum;//最大敌人生成数目
    static int exCurry = 0;
    private float exCurryTime = 0f;

    private float enemyRate;
    private float nextEnemy;

    private int status;//当前场上阶段，影响生成模式，暂时不知道有什么阶段（

    //测试用变量
    public int time;

    // Start is called before the first frame update
    void Start()
    {
        Init();
        //Cmd_create_enemy(pos);
    }

    // Update is called once per frame
    void Update()
    {
        if (Pause.GetInstance().GetState() == false)
        {
            enemyRate = (3 + (1 / 8) * time * time);
            //Debug.Log(enemyRate);

            if (EnemyCurrentNum == 20 || EnemyCurrentNum == 40 || EnemyCurrentNum == 60)
            {
                exCurry = 1;//都给我去放大招啊啊啊啊啊
                exCurryTime = Time.time + 15f;//蓄力射击阶段持续15秒
            }

            if (Time.time > exCurryTime)
            {
                exCurry = 0;
            }

            if (Time.time > nextEnemy && status == 0 && exCurry != 1)
            {
                createEnemy(rangePos(pos));
            }
        }
    }

    void Init()
    {
        pos = this.transform;
        enemyRate = 3f;
        nextEnemy = 0.0f;
        status = 0;
    }

    Vector3 rangePos(Transform pos)
    {
        Vector3 vec3 = pos.position;
        vec3.x += Random.Range(-5,5);
        vec3.y += Random.Range(-5, 5);
        return vec3;
    }

    bool createEnemy(Transform pos)
    {
        GameObject enemy = Instantiate(EnemyPre ,pos.position ,Quaternion.identity);
        if(enemy == null)
        {
            return false;
        }

        EnemyCurrentNum += 1;
        nextEnemy = Time.time + enemyRate;
        return true;
    }//在postion位置生成敌人

    bool createEnemy(Vector3 vec3)
    {
        GameObject enemy = Instantiate(EnemyPre,vec3, Quaternion.identity);
        if (enemy == null)
        {
            return false;
        }

        EnemyCurrentNum += 1;
        nextEnemy = Time.time + enemyRate;
        return true;
    }//在postion位置生成敌人

}

