using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    private GameObject enemy;
    public GameObject player;
    public GameObject bullet;
    private GameObject[] tower = new GameObject[3];
    private int playerFlag;//玩家所在塔的下标

    private float fireRate;//开火间隔 1.5s
    private float nextFire;

    public float speed;//小怪移动速度
    private float moveRate;//移动间隔6s
    private float nextMove;//

    private int status; //为0则正常射击，为1则正在移动中，为2则停止射击开始蓄力exCurry
    float distance;//小怪与玩家所在塔的距离
    float attackDistance;//距离小于此时才可攻击

    Vector3 directionRange = new Vector3(0f, 0f, 0f);//6身位移动用

    struct leagalArea//漫游区
    {
        float xMin, xMax;
        float yMin, yMax;
        float zMin, zMax;
    }

    public Vector3 vec3;//testVector3

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        //获取玩家所在塔
        //playerFlag = getTower(tower);
        //distance = getDistance();

        //移动 test
        //Vector3 direction = getDirection(tower[0]);
        //moveTo(direction, speed);
        //moveT(new Vector3(0f,0f,0f));//停止不动

        //六身位移动流
        if (Pause.GetInstance().GetState() == false)
        {
            MonsterDeath();
            if (Time.time > nextMove && status == 0)
            {
                directionRange = -directionRange;
                status = 1;
                nextMove = Time.time + moveRate;
            }
            /*
            if (Time.time > nextMove && status == 0)//若移动间隔满足且当前不处于移动中则进行新一轮的移动
            {
                
                while (!leagalMove(directionRange))
                {
                    directionRange = getRangeMoveVector3();
                }
                status = 1;//进入移动
                nextMove = Time.time + moveRate;
            }
            */
            if (status == 1)//移动中
            {
                moveTo(directionRange, speed);
                transform.LookAt(tower[playerFlag].transform);//移动后重新调整面向
                if (this.transform.position == directionRange || Time.time > nextMove)
                {
                    status = 0;//达到目的后重新回到日常状态
                    transform.LookAt(tower[playerFlag].transform);//移动后重新调整面向
                    nextMove = Time.time + 8f;//每次移动约3秒,进入移动状态8s后再进行下一次移动
                }
            }
            //Debug.Log(status+":"+directionRange);
            //面向始终朝向塔 test
            //transform.LookAt(tower[playerFlag].transform);
            //transform.LookAt(tower[0].transform);

            //攻击
            //if (Time.time > nextFire && status == 0 && distance < attackDistance)
            if (Time.time > nextFire && (status == 0 || status == 1))//当处于正常允许射击状态，并且在射击频率之外，以及距离小于一定值，才可以射击
            {
                attackByShoot();
            }

            if (status == 2) exCurry();
        }
    }

    void Init()
    {
        player = GameObject.Find("Player");
        enemy = gameObject;
        for (int step = 0; step < 3; step++)
        {
            tower[step] = GameObject.Find("platform").transform.GetChild(step).gameObject;
        }
        playerFlag = 1;
        status = 0;//默认为'normal'状态
        fireRate = 1.5f;
        nextFire = 0.0f;
        moveRate = 6.0f;//6秒移动一次
        nextMove = 0.0f;
        transform.LookAt(tower[playerFlag].transform);//初始朝向面向玩家
        //speed = 2f;
        directionRange = getRangeMoveVector3();
    }

    void moveByRange()
    {
        Vector3 direction = getRangeMoveVector3();
        //Debug.Log(direction);
        while (!leagalMove(direction))
        {
            direction = getRangeMoveVector3();
        }

        moveTo(direction,speed);

        nextMove = Time.time + moveRate;
    }

    void attackByShoot()//
    {
       // GameObject pos = GameObject.Find("bulletPos");
        GameObject pos = this.transform.Find("bulletPos").gameObject;
        GameObject clone;
        clone = Instantiate(bullet, pos.transform.position, pos.transform.rotation);
        //clone = getBullet();

        nextFire = Time.time + fireRate;
    }//射击

    void exCurry()//未完成！：进行集体蓄力攻击
    {

    }

    float getDistance()
    {
        float distance = Vector3.Distance(enemy.transform.position, tower[playerFlag].transform.position);
        return distance;
    }

    //int getTower(GameObject [] tower)
    /*
     {
         for (int i = 0; i < tower.Length; i++)
         {
             if (tower.ifPlayerExist())
             {
                 return i;
             }
         }
         return -1;//三座塔中都没有玩家
     }//获取玩家存在的塔的下标
 *///获取玩家所在塔的下标，暂无玩家信息故不使用

    Vector3 getDirection(GameObject dest)//获取前往目标position的Vector3
    {
        Vector3 resultVec3 = dest.transform.position - enemy.transform.position;
        if (resultVec3 == null)
        {
            return new Vector3(0, 0, 0);
        }
        return Vector3.Normalize(resultVec3);
    }

    void moveTo(Vector3 direction, float speed)//向direction方向移动,direction = zeors时不动
    {
        enemy.transform.Translate(direction * speed * Time.deltaTime);
    }

    Vector3 getRangeMoveVector3()//返回一个随机的方向用以移动
    {
        Vector3 resultVector3 = new Vector3(0f, 0f, 0f);
        resultVector3.x = Random.Range(-10, 10) ;
        resultVector3.y = Random.Range(-10, 10) ;
        resultVector3.z = Random.Range(-10, 10) ;

        return Vector3.Normalize(resultVector3);
    }

    bool leagalMove(Vector3 direction)//判定移动是否合法,不知道漫游区，未完成
    {
        return true;
    }

    private void MonsterDeath()
    {
        if (GetComponent<DamageSystem>().GetCurState() == DamageState.DEATH)
        {
            Destroy(gameObject);
        }
    }
}
