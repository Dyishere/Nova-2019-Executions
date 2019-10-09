using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSystem : MonoBehaviour
{
    public DamageSystemType curType;

    [Header("具体属性设定（若curType为NONE时可自定义）")]
    [SerializeField] private float maxHealth;                  //血量上限
    [SerializeField] private float firstPeriodHealth;          //一阶段血量分界线
    [SerializeField] private float secondPeriodHealth;         //二阶段血量分界线
    [SerializeField] private float curHealth;                  //当前血量
    [SerializeField] private DamageState curState;           //当前血量状态
    private bool isProtect;

    // Start is called before the first frame update
    void Start()
    {
        InitDamageSystem();
    }

    // Update is called once per frame
    void Update()
    {
        if (Pause.GetInstance().GetState() == false)
        {
            CheckHealth();
        }
    }

    /// <summary>
    /// 返回该系统的实际血量。
    /// </summary>
    /// <returns>血量的int类型</returns>
    public float GetCurHealth()
    {
        return curHealth;
    }

    /// <summary>
    /// 该系统血量所处状态。
    /// </summary>
    /// <returns>血量状态枚举</returns>
    public DamageState GetCurState()
    {
        return curState;
    }

    /// <summary>
    /// 造成该系统的伤害。
    /// </summary>
    /// <param name="num">大于零的int类型</param>
    public void Damage(float num)
    {
        if (num < 0 || curState == DamageState.PROTECT || curState == DamageState.DEATH)
        { 
            return;
        }
        curHealth -= num;
    }

    /// <summary>
    /// 造成该系统的恢复。（传入值应大于零）
    /// </summary>
    /// <param name="num">大于零的int类型</param>
    public void Recover(int num)
    {
        if (num <= 0 || curState == DamageState.DEATH)
        {
            Debug.Log("造成回血的输入不合法");
            return;
        }
        curHealth += num;
    }

    /// <summary>
    /// 使该系统处于保护状态，不受伤害。
    /// </summary>
    public void Protect()
    {
        isProtect = !isProtect;
    }

    /// <summary>
    /// 初始化伤害系统数据，当系统类型为NONE时将按照自定义内容。
    /// </summary>
    private void InitDamageSystem()
    {
        switch (curType)
        {
            case DamageSystemType.NONE:
                break;
            case DamageSystemType.BOSS:
                maxHealth = 90000;
                firstPeriodHealth = 60000;
                secondPeriodHealth = 30000;
                break;
            case DamageSystemType.MONSTER:
                maxHealth = 40;
                firstPeriodHealth = 0;
                secondPeriodHealth = 0;
                break;
            case DamageSystemType.TOWER:
                maxHealth = 300;
                firstPeriodHealth = 200;
                secondPeriodHealth = 100;
                break;
        }
        curHealth = maxHealth;
        curState = DamageState.FIRSTPERIOD;
    }

    /// <summary>
    /// 刷新当前伤害系统状况。
    /// </summary>
    private void CheckHealth()
    {

        if (isProtect)
        {
            curState = DamageState.PROTECT;
            return;
        }
        if (curHealth <= 0)
        {
            curHealth = 0;
            curState = DamageState.DEATH;
            return;
        }
        else if (curHealth <= secondPeriodHealth)
        {
            curState = DamageState.THIRDPERIOD;
            return;
        }
        else if (curHealth <= firstPeriodHealth)
        {
            curState = DamageState.SECONDPERIOD;
            return;
        }
        else if (curHealth <= maxHealth)
        {
            curState = DamageState.FIRSTPERIOD;
            return;
        }
    }
}
public enum DamageSystemType
{
    NONE, BOSS, MONSTER, TOWER, 
}
public enum DamageState
{
    DEATH, FIRSTPERIOD, SECONDPERIOD, THIRDPERIOD, PROTECT,
}
