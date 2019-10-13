using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class projectileActor : MonoBehaviour {
    public float perTemperature;            // 每秒无发射的下降速度
    private bool shootingSpeedUpSwitch;     // 用于接收是否提速的信息。
    public  float temperature;              // 温度系统
    public float curTime;                   // 用于计时
    public bool isColdDown;                // 用于判断是否进入过热冷却
    private GameObject[] tower = new GameObject[3];
    private Shaker m_Shaker;
    public Transform spawnLocator; 
    public Transform spawnLocatorMuzzleFlare;
    public Transform shellLocator;
    public Animator recoilAnimator;

    public Transform[] shotgunLocator;

    [System.Serializable]
    public class projectile
    {
        public string name;
        public Rigidbody bombPrefab;
        public GameObject muzzleflare;
        public float min, max;
        public bool rapidFire;
        public float rapidFireCooldown;   

        public bool shotgunBehavior;
        public int shotgunPellets;
        public GameObject shellPrefab;
        public bool hasShells;
    }
    public projectile[] bombList;


    string FauxName;
    public Text UiText;

    public bool UImaster = true;
    public bool CameraShake = true;
    public float rapidFireDelay;
    public CameraShake CameraShakeCaller;

    float firingTimer;
    public bool firing;
    public int bombType = 0;

   // public ParticleSystem muzzleflare;

    public bool swarmMissileLauncher = false;
    int projectileSimFire = 1;

    public bool Torque = false;
    public float Tor_min, Tor_max;

    public bool MinorRotate;
    public bool MajorRotate = false;
    int seq = 0;


	// Use this for initialization
	void Start ()
    {

        //if (UImaster)
        //{
        //    UiText.text = bombList[bombType].name.ToString();
        //}
        InitGun();
    }
	
	// Update is called once per frame
	void Update ()
    {
        //if (Input.GetButtonDown("Fire1"))
        //    Switch(-1);
        //Movement
        if (Pause.GetInstance().GetState() == false)
        {
            if (SteamVR_Actions.default_GrabPinch.stateDown == true)
            {
                firing = true;
            }
            if (SteamVR_Actions.default_GrabPinch.stateUp == true)
            {
                firing = false;
                firingTimer = 0;
            }
            if (bombList[bombType].rapidFire && firing)
            {
                rapidFireDelay = shootingSpeedUpSwitch ? 0 : 0.01f;
                if (firingTimer > bombList[bombType].rapidFireCooldown + rapidFireDelay)
                {
                    Fire();
                    firingTimer = 0;
                }
            }
            if (firing)
            {
                firingTimer += Time.deltaTime;
            }
        }
	}
    private void InitGun()
    {
        if (swarmMissileLauncher)
        {
            projectileSimFire = 1;
        }
        shootingSpeedUpSwitch = false;
        m_Shaker = new Shaker();
        if (GameObject.Find("platform") != null)
            for (int step = 0; step < 3; step++)
            {
                tower[step] = GameObject.Find("platform").transform.GetChild(step).gameObject;
            }
    }
    private void FixedUpdate()
    {
        ColdDown();
    }
    public void ShootingSpeedUp(bool curSwitch)
    {
        shootingSpeedUpSwitch = curSwitch;
    }
    private void ColdDown()
    {
        if (curTime > 120f)
        {
            curTime = 0f;
            temperature -= perTemperature;
        }
        if (temperature < 0)
        {
            temperature = 0f;
        }
        curTime += 1f;
    }
    public void Switch(int value)
    {
            bombType += value;
            if (bombType < 0)
            {
              bombType = bombList.Length;
              bombType--;
            }
            else if (bombType >= bombList.Length)
            {
                bombType = 0;
            }
       // if (UImaster)
       // {
       //     UiText.text = bombList[bombType].name.ToString();
       // }
    }

    public void Fire()
    {
        if (GameObject.Find("platform") != null)
        {
            if (tower[PlayerInPlatform.GetInstance().GetPlatform()].GetComponent<DamageSystem>().GetCurState() == DamageState.DEATH)
            {
                Debug.Log("该塔已损坏，请撤离");
                return;
            }
            if (temperature > 100f && !isColdDown)
            {
                StartCoroutine(ShootingColdDown());
                temperature--;
                Debug.Log("枪支过热");
                return;
            }
            if (isColdDown)
            {
                Debug.Log("冷却中");
                return;
            }
        }

        curTime = 0;
        m_Shaker.RightHandShake();
        temperature += 0.3f;
        //if(CameraShake)
        //{
        //    CameraShakeCaller.ShakeCamera();
        //}
        Instantiate(bombList[bombType].muzzleflare, spawnLocatorMuzzleFlare.position, spawnLocatorMuzzleFlare.rotation);
        //   bombList[bombType].muzzleflare.Play();

        if (bombList[bombType].hasShells)
        {
            Instantiate(bombList[bombType].shellPrefab, shellLocator.position, shellLocator.rotation);
        }
        recoilAnimator.SetTrigger("recoil_trigger");

        Rigidbody rocketInstance;
        rocketInstance = Instantiate(bombList[bombType].bombPrefab, spawnLocator.position,spawnLocator.rotation) as Rigidbody;
        // Quaternion.Euler(0,90,0)
        rocketInstance.AddForce(spawnLocator.forward * Random.Range(bombList[bombType].min, bombList[bombType].max));

        if (bombList[bombType].shotgunBehavior)
        {
            for(int i = 0; i < bombList[bombType].shotgunPellets ;i++ )
            {
                Rigidbody rocketInstanceShotgun;
                rocketInstanceShotgun = Instantiate(bombList[bombType].bombPrefab, shotgunLocator[i].position, shotgunLocator[i].rotation) as Rigidbody;
                // Quaternion.Euler(0,90,0)
                rocketInstanceShotgun.AddForce(shotgunLocator[i].forward * Random.Range(bombList[bombType].min, bombList[bombType].max));
            }
        }
        /*
        if (Torque)
        {
            rocketInstance.AddTorque(spawnLocator.up * Random.Range(Tor_min, Tor_max));
        }
        if (MinorRotate)
        {
            RandomizeRotation();
        }
        if (MajorRotate)
        {
            Major_RandomizeRotation();
        }*/
    }
    IEnumerator ShootingColdDown()
    {
        isColdDown = true;
        yield return new WaitForSeconds(5);
        isColdDown = false;
    }

    void RandomizeRotation()
    {
        if (seq == 0)
        {
            seq++;
            transform.Rotate(0, 1, 0);
        }
      else if (seq == 1)
        {
            seq++;
            transform.Rotate(1, 1, 0);
        }
      else if (seq == 2)
        {
            seq++;
            transform.Rotate(1, -3, 0);
        }
      else if (seq == 3)
        {
            seq++;
            transform.Rotate(-2, 1, 0);
        }
       else if (seq == 4)
        {
            seq++;
            transform.Rotate(1, 1, 1);
        }
       else if (seq == 5)
        {
            seq = 0;
            transform.Rotate(-1, -1, -1);
        }
    }

    void Major_RandomizeRotation()
    {
        if (seq == 0)
        {
            seq++;
            transform.Rotate(0, 25, 0);
        }
        else if (seq == 1)
        {
            seq++;
            transform.Rotate(0, -50, 0);
        }
        else if (seq == 2)
        {
            seq++;
            transform.Rotate(0, 25, 0);
        }
        else if (seq == 3)
        {
            seq++;
            transform.Rotate(25, 0, 0);
        }
        else if (seq == 4)
        {
            seq++;
            transform.Rotate(-50, 0, 0);
        }
        else if (seq == 5)
        {
            seq = 0;
            transform.Rotate(25, 0, 0);
        }
    }
}
